using System;
using NLog;
using Pixstock.Nc.Srv.Gateway;
using Pixstock.Nc.Srv.Gateway.Repository;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Model;
using SimpleInjector.Lifestyles;
using Xunit;

namespace Pixstock.Nc.Srv.Tests.Test
{
    public class EntityLabelTest : EntityTestBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public EntityLabelTest()
        {
        }

        /// <summary>
        /// Label読み込みテスト
        /// </summary>
        [Fact]
        public void Test_Load_01()
        {
            _logger.Trace(">>> Labelモデルの読み込みテストを開始");
            using (InitializeFact())
            {
                var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                var repo = new LabelRepository(dbc);
                var entity = repo.Load(2L);
                Assert.Equal(entity.Name, "テストラベル_2");

                Assert.Equal(entity.Category.Id, 1L);
                Assert.Equal(entity.Category.Name, "ROOT");
                Assert.Equal(entity.Contents[0].Content.Id, 1L);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_Update_01()
        {
            _logger.Trace(">>> Labelモデルの読み込みテストを開始");
            using (InitializeFact())
            {
                string updateName = "POTEST";
                {
                    var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                    var repo = new LabelRepository(dbc);
                    var entity = repo.Load(2L);
                    entity.Name = updateName;
                    repo.Save();
                }
                {
                    var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                    var repo = new LabelRepository(dbc);
                    var entity = repo.Load(2L);
                    Assert.Equal(entity.Name, updateName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_RelationUpdate_01()
        {
            _logger.Trace(">>> Labelモデルの読み込みテストを開始");
            using (InitializeFact())
            {
                {
                    var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                    var repo = new LabelRepository(dbc);
                    var repoc = new ContentRepository(dbc);
                    var entity = repo.Load(2L);

                    var content = repoc.Load(2L);
                    entity.Contents.Add(new Model.Label2Content
                    {
                        Content = (Content)content,
                        Label = entity
                    });

                    repo.Save();
                }

                {
                    var @dbc = (AppDbContext)container.GetInstance<IAppDbContext>();
                    var repo = new LabelRepository(dbc);
                    var entity = repo.Load(2L);
                    Assert.Equal(entity.Contents[1].Content.Id, 2L);
                }
            }
        }
    }
}