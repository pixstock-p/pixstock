using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;
using Pixstock.Nc.Srv.Infra.Model;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Lifestyles;

namespace Pixstock.Nc.Srv.Ext
{
    /// <summary>
    /// 拡張機能の読み込みや操作するクラス
    /// </summary>
    public class ExtentionManager
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public readonly Container container;

        private readonly Container extention_container;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExtentionManager(Container container)
        {
            this.container = container;
            this.extention_container = new Container();

            container.Register<IExtentionRunner, ExtentionRunner>(Lifestyle.Singleton);
        }

        /// <summary>
        /// 拡張機能ライブラリファイルの読み込み
        /// </summary>
        /// <param name="pluginDirectory"></param>
        public void InitializePlugin(string pluginDirectory)
        {
            try
            {
                var pluginAssemblies =
                    from file in new DirectoryInfo(pluginDirectory).GetFiles()
                    where file.Extension.ToLower() == ".pex"
                    select Assembly.LoadFile(file.FullName);
                if (pluginAssemblies.Count() > 0)
                {
                    _logger.Info("拡張機能({0})の読み込みを開始します。", pluginDirectory);
                    extention_container.RegisterCollection<IExtentionMetaInfo>(pluginAssemblies);
                }
            }
            catch (IOException e)
            {
                _logger.Warn(e);
                _logger.Warn("拡張機能の読み込みに失敗しました。 pluginDirectory={0}", pluginDirectory);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginClazz"></param>
        public void AddPlugin(Type pluginClazz)
        {
            extention_container.AppendToCollection(typeof(IExtentionMetaInfo), pluginClazz);
        }

        /// <summary>
        /// 拡張機能からカットポイント別インターフェース実装クラスを登録する
        /// </summary>
        public void CompletePlugin()
        {
            extention_container.Verify();

            RegisterCutpoint(typeof(DefaultCutpoint));

            try
            {
                var it = extention_container.GetAllInstances<IExtentionMetaInfo>();
                foreach (var prop in it)
                {
                    foreach (var cutpoint in prop.Cutpoints())
                    {
                        RegisterCutpoint(cutpoint);
                    }

                }
            }
            catch (SimpleInjector.ActivationException)
            {
                _logger.Warn("拡張機能のインターフェース取得に失敗しました。");
            }
        }

        public void Execute(ExtentionCutpointType cutpoint, object param)
        {
            switch (cutpoint)
            {
                case ExtentionCutpointType.START:
                    Execute_START(param);
                    break;
                case ExtentionCutpointType.API_GET_CATEGORY:
                    Execute_API_GET_CATEGORY(param);
                    break;
            }

        }

        private void Execute_START(object param)
        {
            var ite = container.GetAllInstances<IStartCutpoint>();
            foreach (var prop in ite)
            {
                try
                {
                    prop.Process(param);
                }
                catch (Exception expr)
                {
                    _logger.Warn("拡張機能の実行でエラーは発生しました。");
                    _logger.Warn(expr);
                }
            }
        }

        private void Execute_API_GET_CATEGORY(object param)
        {
            var ite = container.GetAllInstances<ICategoryApiCutpoint>();
            foreach (var prop in ite.Select(p => (ICategoryApiCutpoint)p))
            {

                try
                {
                    prop.OnGetCategory((ICategory)param);
                }
                catch (Exception expr)
                {
                    _logger.Warn("拡張機能の実行でエラーは発生しました。");
                    _logger.Warn(expr);
                }
            }
        }

        private void RegisterCutpoint(Type cutpoint)
        {
            if ((typeof(IStartCutpoint)).IsAssignableFrom(cutpoint))
            {
                container.AppendToCollection(typeof(IStartCutpoint), cutpoint);
            }

            if ((typeof(ICategoryApiCutpoint)).IsAssignableFrom(cutpoint))
            {
                container.AppendToCollection(typeof(ICategoryApiCutpoint), cutpoint);
            }
        }

    }
}