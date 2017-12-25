using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using Pixstock.Nc.Common;
using Pixstock.Nc.Srv.Gateway;
using Pixstock.Nc.Srv.Gateway.Repository;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Pixstock.Nc.Srv.Tests
{
    public class EntityTestBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        protected SimpleInjector.Container container;

        static object TestThreadLocker = new object();

        protected readonly string TESTDATADIRECTORY;

        public EntityTestBase()
        {
            TESTDATADIRECTORY = Path.GetTempPath();
        }

        protected Scope InitializeFact(bool isUsingInMemoryDatabase = true)
        {
            Scope scope = null;
            lock (TestThreadLocker)
            {
                int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

                InitializeContainer(threadId);
                if (isUsingInMemoryDatabase)
                {
                    container.Register<IAppDbContext, TestAppDbContext>(Lifestyle.Scoped);
                }
                else
                {
                    container.Register<IAppDbContext, AppDbContext>(Lifestyle.Scoped);
                }

                container.Verify();



                scope = AsyncScopedLifestyle.BeginScope(container);

                var context = (AppDbContext)container.GetInstance<IAppDbContext>();

                if (isUsingInMemoryDatabase)
                {
                    // In-Memory Database
                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();
                }

                var app = (ApplicationContextImpl)container.GetInstance<IApplicationContext>();
                app.Initialize();

                System.Console.WriteLine("アプリケーションフォルダ=" + app.ApplicationDirectoryPath);

                ImportInitializeData(threadId, container);

                System.Console.WriteLine("データベースの初期化を完了しました。");
            }

            return scope;
        }

        /// <summary>
        /// InMemoryデータベース用のユニットテストデータを作成する
        /// </summary>
        void ImportInitializeData(int threadId, SimpleInjector.Container container)
        {
            var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();

            // Category
            var repo_Category = new CategoryRepository(@dbc);
            repo_Category.Add(new Category
            {
                Id = 2,
                Name = "テストカテゴリA"
            });
            repo_Category.Add(new Category
            {
                Id = 3,
                Name = "テストカテゴリA2"
            });
            repo_Category.Add(new Category
            {
                Id = 4,
                Name = "テストカテゴリA3"
            });

            // Label
            var repo_Label = new LabelRepository(@dbc);
            repo_Label.Add(new Label
            {
                Id = 1,
                Name = "テストラベル"
            });
            repo_Label.Add(new Label
            {
                Id = 2,
                Name = "テストラベル_2",
                Category = (Category)repo_Category.Load(1)
            });

            // Content
            var repo_Content = new ContentRepository(@dbc);
            repo_Content.Add(new Content
            {
                Id = 1,
                Name = "Content1",
                IdentifyKey = "IDEN_Content1"
            });
            repo_Content.Add(new Content
            {
                Id = 2,
                Name = "Content2",
                IdentifyKey = "IDEN_Content2",

            });
            repo_Content.Add(new Content
            {
                Id = 3,
                Name = "Content3",
                IdentifyKey = "IDEN_Content3"
            });

            // Workspace
            var repo_Workspace = new WorkspaceRepository(@dbc);
            var workspace1 = new Workspace
            {
                Id = 1,
                Name = "UT_Workspace",
                PhysicalPath = Path.Combine(TESTDATADIRECTORY, "PixstockSrvUT_" + threadId)
            };
            repo_Workspace.Add(workspace1);

            // FileMappingInfo
            var repo_FileMappingInfo = new FileMappingInfoRepository(@dbc);
            repo_FileMappingInfo.Add(new FileMappingInfo
            {
                Id = 1,
                AclHash = "ABC1",
                Workspace = workspace1
            });

            @dbc.SaveChanges();

            var label2 = repo_Label.Load(2L);
            label2.Contents.Add(new Label2Content
            {
                ContentId = 1L,
                Content = (Content)repo_Content.Load(1L),
                LabelId = 2L,
                Label = label2
            });

            @dbc.SaveChanges();
        }

        /// <summary>
        /// ユニットテスト用のSQLファイルを読み込む
        /// </summary>
        void ImportSqlFile(SimpleInjector.Container container, string ImportSqlFileName)
        {
            string sqltext = "";
            System.Reflection.Assembly assm = System.Reflection.Assembly.GetExecutingAssembly();

            using (var stream = assm.GetManifestResourceStream(string.Format("Pixstock.Nc.Srv.Tests.Assets.Sql.{0}", ImportSqlFileName)))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    sqltext = reader.ReadToEnd();
                }
            }

            if (!string.IsNullOrEmpty(sqltext))
            {
                var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                @dbc.Database.ExecuteSqlCommand(sqltext);
                @dbc.SaveChanges();
            }
        }

        /// <summary>
        /// DIコンテナの作成
        /// </summary>
        /// <remarks>
        /// 試験で追加のコンテナ登録が必要な場合は、このメソッドをオーバーライドし内部でこのメソッドを呼び出します。
        /// </remarks>
        protected virtual void InitializeContainer(int threadId)
        {
            container = new SimpleInjector.Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var assemblyParameter = new TestBuildAssemblyParameter();
            assemblyParameter.SetThread(threadId);

            var context = new ApplicationContextImpl(assemblyParameter); // アプリケーションコンテキスト
            context.SetDiContainer(container);

            container.RegisterSingleton<IApplicationContext>(context);
            container.RegisterSingleton<IBuildAssemblyParameter>(assemblyParameter);
            container.Register<ICategoryRepository, CategoryRepository>();
            container.Register<IContentRepository, ContentRepository>();
            container.Register<IFileMappingInfoRepository, FileMappingInfoRepository>();
            container.Register<IWorkspaceRepository, WorkspaceRepository>();
            container.Register<IAppAppMetaInfoRepository, AppAppMetaInfoRepository>();
        }

        protected void DumpDatabase()
        {
            var dbc = (AppDbContext)container.GetInstance<IAppDbContext>();

            foreach (var entity in dbc.Contents)
            {
                _logger.Info("Id={0} Name={1}", entity.Id, entity.Name);
            }
        }
    }
}