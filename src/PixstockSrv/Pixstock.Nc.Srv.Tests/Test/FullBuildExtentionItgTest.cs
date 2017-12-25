using System.IO;
using NLog;
using Pixstock.Nc.Srv.Ext;
using Pixstock.Nc.Srv.Ext.FullBuild;
using Xunit;

namespace Pixstock.Nc.Srv.Tests.ItgTest
{
    /// <summary>
    /// ITG
    /// </summary>
    public class FullBuildExtentionItgTest : EntityTestBase
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
        public void FullBuildExtention_PhyEfDB_Process_Test()
        {
            _logger.Info(">>> FullBuildExtention_PhyEfDB_Process_Test");

            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            string temporaryBasePath = Path.Combine(TESTDATADIRECTORY, "PixstockSrvUT_" + threadId);
            BuildTemporaryFiles(temporaryBasePath);

            using (InitializeFact(false))
            {
                var obj = container.GetInstance<StartCutpoint>();
                obj.Process(new CutpointStartParameter {WorkspaceId = 1L});
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