using System;
using Pixstock.Nc.Srv.Gateway;
using Pixstock.Nc.Srv.Gateway.Repository;
using Pixstock.Nc.Srv.Infra;
using SimpleInjector.Lifestyles;
using Xunit;
using NLog;

namespace Pixstock.Nc.Srv.Tests
{
    public class EntityCategoryTest : EntityTestBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public EntityCategoryTest()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_Save_EntityField()
        {
            _logger.Trace(">>> Categoryモデルの読み込みテストを開始");

            using (InitializeFact())
            {
                {
                    var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                    var repo = new CategoryRepository(dbc);
                    var category = repo.Load(2L);
                    category.Name = "POTEST";
                    repo.Save();
                }

                {
                    var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                    var repo = new CategoryRepository(dbc);
                    var category = repo.Load(2L);

                    Assert.Equal(category.Name, "POTEST");
                }
            }
        }
    }
}
