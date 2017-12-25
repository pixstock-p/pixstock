using System;
using System.IO;
using NLog;
using Pixstock.Nc.Srv.Ext;
using Pixstock.Nc.Srv.Ext.FullBuild;
using Pixstock.Nc.Srv.Gateway;
using Pixstock.Nc.Srv.Gateway.Repository;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Lifestyles;
using Xunit;
using Xunit.Abstractions;

namespace Pixstock.Nc.Srv.Tests.Test
{

    public class FullBuildExtentionTest : EntityTestBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        ExtentionManager manager;

        protected override void InitializeContainer(int threadId)
        {
            base.InitializeContainer(threadId);
            this.manager = new ExtentionManager(container);
            this.manager.AddPlugin(typeof(FullBuildExtention));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void FullBuildExtention_Process_Test()
        {
            _logger.Trace(">>> 拡張機能の読み込みテストを開始");

            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            string temporaryBasePath = Path.Combine(TESTDATADIRECTORY, "PixstockSrvUT_" + threadId);
            BuildTemporaryFiles(temporaryBasePath);

            using (InitializeFact())
            {
                var obj = container.GetInstance<StartCutpoint>();
                obj.Process(new CutpointStartParameter { WorkspaceId = 1L });

                DumpDatabase();

                // **** Categoryのチェック
                var categoryRepository = container.GetInstance<ICategoryRepository>();

                // 「Data/*」のファイルを格納したカテゴリ
                var entity_1 = categoryRepository.LoadByName("Data");
                Assert.NotNull(entity_1);
                _logger.Trace("Category: Id={0}, Name={1}", entity_1.Id, entity_1.Name);
                Assert.Equal("Data.txt", entity_1.GetContentList()[0].Name);
                Assert.Equal("Data2.txt", entity_1.GetContentList()[1].Name);

                // 「Data/A/*」のファイルを格納したカテゴリ
                var entity_2 = categoryRepository.LoadByName("A");
                Assert.NotNull(entity_2);
                _logger.Trace("Category: Id={0}, Name={1}", entity_2.Id, entity_2.Name);
                Assert.Equal("Data.txt", entity_2.GetContentList()[0].Name);

                // 「Data/B/*」のファイルを格納したカテゴリ
                var entity_3 = categoryRepository.LoadByName("B");
                Assert.NotNull(entity_3);
                _logger.Trace("Category: Id={0}, Name={1}", entity_3.Id, entity_3.Name);
                Assert.Equal("B.txt", entity_3.GetContentList()[0].Name);
                Assert.Equal("B2.txt", entity_3.GetContentList()[1].Name);

                // 「Data/B/A/*」のファイルを格納したカテゴリ
                var entity_4 = categoryRepository.LoadByName("B_A");
                Assert.NotNull(entity_4);
                _logger.Trace("Category: Id={0}, Name={1}", entity_4.Id, entity_4.Name);
                Assert.Equal("B_A.txt", entity_4.GetContentList()[0].Name);
                Assert.Equal("B_A2.txt", entity_4.GetContentList()[1].Name);

                // 「Data/B/A/B_A/*」のファイルを格納したカテゴリ
                var entity_5 = categoryRepository.LoadByName("A_B_A");
                Assert.NotNull(entity_5);
                _logger.Trace("Category: Id={0}, Name={1}", entity_5.Id, entity_5.Name);
                Assert.Equal("B_A_B_A.txt", entity_5.GetContentList()[0].Name);
            }
        }

        /// <summary>
        /// 試験用のダミーディレクトリおよびダミーファイルの作成
        /// </summary>
        /// <param name="workspacePath">試験で使用するフォルダパス</param>
        private void BuildTemporaryFiles(string workspacePath)
        {
            Directory.CreateDirectory(Path.Combine(workspacePath, "Data"));
            File.Create(Path.Combine(workspacePath, "Data/Data.txt")).Close();
            File.Create(Path.Combine(workspacePath, "Data/Data2.txt")).Close();

            Directory.CreateDirectory(Path.Combine(workspacePath, "Data/A"));
            File.Create(Path.Combine(workspacePath, "Data/A/Data.txt")).Close();

            Directory.CreateDirectory(Path.Combine(workspacePath, "Data/B"));
            File.Create(Path.Combine(workspacePath, "Data/B/B.txt")).Close();
            File.Create(Path.Combine(workspacePath, "Data/B/B2.txt")).Close();

            Directory.CreateDirectory(Path.Combine(workspacePath, "Data/B/A"));
            File.Create(Path.Combine(workspacePath, "Data/B/A/B_A.txt")).Close();
            File.Create(Path.Combine(workspacePath, "Data/B/A/B_A2.txt")).Close();

            Directory.CreateDirectory(Path.Combine(workspacePath, "Data/B/A/B_A"));
            File.Create(Path.Combine(workspacePath, "Data/B/A/B_A/B_A_B_A.txt")).Close();
        }
    }


}