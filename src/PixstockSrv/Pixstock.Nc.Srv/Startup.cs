using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pixstock.Nc.Common;
using Pixstock.Nc.Core;
using Pixstock.Nc.Srv.Ext;
using Pixstock.Nc.Srv.Gateway;
using Pixstock.Nc.Srv.Gateway.Repository;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Core;
using Pixstock.Nc.Srv.Infra.Repository;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Pixstock.Nc.Srv
{
    public class Startup
    {
        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);

            container.Register<ErrorExceptionResolver>();

            var assemblyParameter = new BuildAssemblyParameter();
            var context = new ApplicationContextImpl(assemblyParameter); // アプリケーションコンテキスト

            context.SetDiContainer(container);
            container.RegisterSingleton<IApplicationContext>(context);
            container.RegisterSingleton<IBuildAssemblyParameter>(assemblyParameter);
            container.Register<ICategoryRepository, CategoryRepository>();
            container.Register<IContentRepository, ContentRepository>();
            container.Register<IFileMappingInfoRepository, FileMappingInfoRepository>();
            container.Register<IWorkspaceRepository, WorkspaceRepository>();
            container.Register<IAppAppMetaInfoRepository, AppAppMetaInfoRepository>();
            container.Register<IThumbnailAppMetaInfoRepository, ThumbnailAppMetaInfoRepository>();
            container.Register<IThumbnailRepository, ThumbnailRepository>();
            container.Register<IAppDbContext, AppDbContext>(Lifestyle.Scoped);
            container.Register<IThumbnailDbContext, ThumbnailDbContext>(Lifestyle.Scoped);
            container.Register<IThumbnailBuilder, ThumbnailBuilder>();

            var extentionManager = new ExtentionManager(container);
            container.RegisterSingleton<ExtentionManager>(extentionManager);
            extentionManager.InitializePlugin(context.ExtentionDirectoryPath);
            extentionManager.CompletePlugin();
            container.Verify();

            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var appCtx = (ApplicationContextImpl)container.GetInstance<IApplicationContext>();
                appCtx.Initialize();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (AsyncScopedLifestyle.BeginScope(container))
            {
                // 拡張機能: START
                extentionManager.Execute(ExtentionCutpointType.START, new CutpointStartParameter { WorkspaceId = 1L });
            }

            app.Use((c, next) => container.GetInstance<ErrorExceptionResolver>().Invoke(c, next));
            app.UseMvc();
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            //container.Register<IUserService, UserService>(Lifestyle.Scoped);

            // Cross-wire ASP.NET services (if any). For instance:
            //container.CrossWire<ILoggerFactory>(app);

            // NOTE: Do prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/
        }
    }
}
