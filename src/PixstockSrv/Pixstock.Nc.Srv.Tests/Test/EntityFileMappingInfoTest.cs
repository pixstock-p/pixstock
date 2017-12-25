using NLog;
using Pixstock.Nc.Srv.Gateway;
using Pixstock.Nc.Srv.Gateway.Repository;
using Pixstock.Nc.Srv.Infra;
using Xunit;

namespace Pixstock.Nc.Srv.Tests.Test
{
    public class EntityFileMappingInfoTest : EntityTestBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// FileMappingInfo読み込みテスト
        /// </summary>
        [Fact]
        public void Test_Load_01()
        {
            _logger.Trace(">>> FileMappingInfoモデルの読み込みテストを開始");
            using (InitializeFact())
            {
                var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                var repo = new FileMappingInfoRepository(dbc);
                var entity = repo.Load(1L);

                Assert.Equal(entity.AclHash, "ABC1");
            }
        }
    }
}