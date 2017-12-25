using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LiteDB;
using NLog;
using Pixstock.Nc.Srv.Ext.FullBuild.Model;
using Pixstock.Nc.Srv.Infra.Core;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;

namespace Pixstock.Nc.Srv.Ext.FullBuild
{
    public class StartCutpoint : IStartCutpoint
    {

        private static NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        static readonly string DbTableName = "FullBuildFileInfoList";

        static readonly string DbFileName = "FullBuildExtentionTemporaryDb";

        ICategoryRepository categoryRepository;

        IContentRepository contentRepository;

        IWorkspaceRepository workspaceRepository;

        IFileMappingInfoRepository fileMappingInfoRepository;

        IAppAppMetaInfoRepository appMetaInfoRepository;

        IThumbnailBuilder thumbnailBuilder;

        public StartCutpoint(ICategoryRepository categoryRepository,
            IContentRepository contentRepository,
            IWorkspaceRepository workspaceRepository,
            IFileMappingInfoRepository fileMappingInfoRepository,
            IAppAppMetaInfoRepository appMetaInfoRepository,
            IThumbnailBuilder thumbnailBuilder)
        {
            this.categoryRepository = categoryRepository;
            this.contentRepository = contentRepository;
            this.workspaceRepository = workspaceRepository;
            this.fileMappingInfoRepository = fileMappingInfoRepository;
            this.appMetaInfoRepository = appMetaInfoRepository;
            this.thumbnailBuilder = thumbnailBuilder;
        }

        public void Process(object param)
        {
            _logger.Info("フルビルド拡張機能を実行します");

            var _param = param as CutpointStartParameter;

            if (_param == null) _logger.Warn("パラメータは必須です");

            var workspace = workspaceRepository.Load(_param.WorkspaceId);
            if (workspace == null)
            {
                _logger.Warn("ワークスペース({0})が見つかりません", _param.WorkspaceId);
                return;
            }

            if (!workspace.LastFullBuildDate.HasValue)
            {
                Cutpoint_START(workspace, workspace.PhysicalPath);
                workspace.LastFullBuildDate = DateTime.Now;
                workspaceRepository.Save();
            }
            else
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileBuildPath">フルビルド開始点のフォルダパス</param>
        private void Cutpoint_START(IWorkspace workspace, string fileBuildPath)
        {
            _logger.Debug("IN");

            // 親カテゴリの作成
            var parentCategory = categoryRepository.New();
            parentCategory.Name = "LastBuild_" + DateTime.Now.ToString("yyyyMMdd");
            parentCategory.SetParentCategory(categoryRepository.LoadRootCategory());

            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (var db = new LiteDatabase(DbFileName))
            {
                db.DropCollection(DbTableName);

                // Get customer collection
                var customers = db.GetCollection<FullBuildFileInfo>(DbTableName);

                var checkDirectory = fileBuildPath;
                foreach (var fileName in Directory.EnumerateFiles(checkDirectory, "*", SearchOption.AllDirectories))
                {
                    var fileInfo = new FileInfo(fileName);

                    if (dict.ContainsKey(fileInfo.Directory.FullName))
                    {
                        // カテゴリは、dict[fileInfo.Directory.FullName]を使用する
                        AddFile(fileInfo, dict[fileInfo.Directory.FullName], customers);
                    }
                    else
                    {
                        var directory = fileInfo.Directory;
                        if (!dict.ContainsValue(directory.Name))
                        {
                            // カテゴリは、directory.Nameを作成する。
                            dict.Add(fileInfo.Directory.FullName, directory.Name);
                            AddFile(fileInfo, directory.Name, customers);
                        }
                        else
                        {
                            bool bAdded = false;
                            StringBuilder sb = new StringBuilder();
                            sb.Append(directory.Name);

                            while (directory.Parent != null)
                            {
                                directory = directory.Parent;
                                sb.Insert(0, directory.Name + "_");

                                if (!dict.ContainsValue(sb.ToString()))
                                {
                                    // カテゴリは、directory.Nameを作成する。
                                    dict.Add(fileInfo.Directory.FullName, sb.ToString());
                                    AddFile(fileInfo, sb.ToString(), customers);
                                    bAdded = true;
                                    break;
                                }
                            }

                            if (!bAdded)
                            {
                                // 最大で１００件まで重複チェックを行う。
                                // ナンバリング値が１００以内でも重複が解消されない場合は、そのフォルダのカテゴリ情報への登録は諦める。
                                for (int i = 0; i < 100; i++)
                                {
                                    string numberedCategoryName = sb.ToString() + "_" + i;
                                    if (!dict.ContainsValue(numberedCategoryName))
                                    {
                                        dict.Add(fileInfo.Directory.FullName, numberedCategoryName);
                                        AddFile(fileInfo, numberedCategoryName, customers);
                                        bAdded = true;
                                        break;
                                    }
                                }

                                if (!bAdded)
                                {
                                    _logger.Warn("フォルダパス「{0}」のカテゴリ情報登録ができませんでした。", fileInfo.Directory.FullName);
                                }
                            }
                        }

                    }
                }

                CreateCategoriesIfNotExists(dict.Values.ToList(), parentCategory); //< カテゴリ情報の作成
            }

            _logger.Info("一時データベースからフルビルドアイテム登録処理");
            using (var db = new LiteDatabase(DbFileName))
            {
                var customers = db.GetCollection<FullBuildFileInfo>(DbTableName);
                // データベースから情報を抜き出し、ファイルマッピング情報の生成を行う。
                InsertEntity(workspace, customers);
            }

            File.Delete(DbFileName); // 一時ファイルを削除
            _logger.Debug("OUT");
        }

        private void InsertEntity(IWorkspace workspace, LiteCollection<FullBuildFileInfo> fullBuildCollection)
        {
            foreach (var prop in fullBuildCollection.FindAll())
            {
                var category = categoryRepository.LoadByName(prop.CategoryName);
                if (category == null)
                {
                    _logger.Warn("カテゴリ({0})を見つけることができませんでした。", prop.CategoryName);
                    throw new ApplicationException();
                }

                var fileInfo = new FileInfo(prop.AbsoluteFilePath);

                // FileMappingInfo作成
                string mimetype = "";
                switch (fileInfo.Extension)
                {
                    case ".png":
                        mimetype = "image/png";
                        break;
                    case ".jpg":
                    case ".jpeg":
                        mimetype = "image/jpg";
                        break;
                    case ".gif":
                        mimetype = "image/gif";
                        break;
                }

                var fileMappingInfo = fileMappingInfoRepository.New();
                fileMappingInfo.AclHash = GenerateACLHash();
                fileMappingInfo.MappingFilePath = workspace.TrimWorekspacePath(fileInfo.FullName);
                fileMappingInfo.Mimetype = mimetype;
                fileMappingInfo.SetWorkspace(workspace);


                // Content作成
                var content = contentRepository.New();
                content.Name = fileInfo.Name;
                content.SetCategory(category);
                content.SetFileMappingInfo(fileMappingInfo);

                // サムネイルの作成
                if (fileInfo.Extension == ".png" ||
                    fileInfo.Extension == ".jpeg" ||
                    fileInfo.Extension == ".jpg" ||
                    fileInfo.Extension == ".gif")
                {
                    string thumbnailHash = thumbnailBuilder.BuildThumbnail(null, fileInfo.FullName);
                    content.ThumbnailKey = thumbnailHash;
                }

            }
            contentRepository.Save();
        }

        /// <summary>
        /// ファイル情報を一時データベースへ格納するサポート関数
        /// </summary>
        /// <param name="file"></param>
        /// <param name="categoryName"></param>
        /// <param name="collection">一時データベースのテーブルコンテキスト</param>
        private void AddFile(FileInfo file, string categoryName, LiteCollection<FullBuildFileInfo> collection)
        {
            collection.Insert(new FullBuildFileInfo
            {
                AbsoluteFilePath = file.FullName,
                CategoryName = categoryName,
                FullBuildDate = DateTime.Now,
            });
        }

        /// <summary>
        /// Categoryエントリの生成
        /// </summary>
        /// <remarks>
        /// 未定義のカテゴリがある場合のみ、指定したカテゴリ名でCategoryエントリを生成する。
        /// </remarks>
        private void CreateCategoriesIfNotExists(List<string> categoryNames, ICategory parentCategory)
        {
            foreach (var categoryName in categoryNames)
            {
                var category = categoryRepository.LoadByName(categoryName);
                if (category == null)
                {
                    var newCategory = categoryRepository.New();
                    newCategory.Name = categoryName;
                    newCategory.SetParentCategory(parentCategory);
                }
            }

            categoryRepository.Save();
        }

        public static string GenerateACLHash()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}