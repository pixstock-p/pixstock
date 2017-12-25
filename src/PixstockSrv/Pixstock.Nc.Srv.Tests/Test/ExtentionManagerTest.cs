using System;
using System.Collections.Generic;
using NLog;
using Pixstock.Nc.Srv.Ext;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Model;
using Xunit;

namespace Pixstock.Nc.Srv.Tests.Test
{
    public class ExtentionManagerTest : EntityTestBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        ExtentionManager manager;

        protected override void InitializeContainer(int threadId)
        {
            base.InitializeContainer(threadId);
            this.manager = new ExtentionManager(container);
            this.manager.AddPlugin(typeof(SampleExtention)); // 試験用のサンプル拡張機能を、プラグインとして追加する。

            this.manager.CompletePlugin();
        }

        /// <summary>
        /// 拡張機能を呼び出し確認
        /// </summary>
        [Fact]
        public void ExecuteExtentionTest()
        {
            _logger.Trace(">>> 拡張機能の読み込みテストを開始");

            using (InitializeFact())
            {
                this.manager.Execute(ExtentionCutpointType.START, 1);
            }
        }

        /// <summary>
        /// 拡張機能を呼び出し確認
        /// </summary>
        [Fact]
        public void ExecuteExtentionTest_CategoryApi()
        {
            _logger.Trace(">>> 拡張機能の読み込みテストを開始");

            using (InitializeFact())
            {
                this.manager.Execute(ExtentionCutpointType.API_GET_CATEGORY, new Category { Id = 1L, Name = "Test Category" });
            }
        }
    }

    /// <summary>
    /// 試験用サンプルプラグイン
    /// </summary>
    public class SampleExtention : IExtentionMetaInfo
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public List<Type> Cutpoints()
        {
            return new List<Type>(new Type[] { typeof(SampleExtention_Start) });
        }
    }

    /// <summary>
    /// 試験用サンプルプラグインが公開しているカットポイントインターフェース
    /// </summary>
    public class SampleExtention_Start : IStartCutpoint, ICategoryApiCutpoint
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void OnGetCategory(ICategory category)
        {
            _logger.Trace("SampleExtention_Start.OnGetCategoryの呼び出し");
        }

        public void Process(object param)
        {
            _logger.Trace("SimpelExtention_StartのProcess呼び出し");
        }
    }
}