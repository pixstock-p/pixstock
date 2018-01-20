﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pixstock.Nc.App.Controllers.Workflow;

namespace Pixstock.Nc.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var option = new BrowserWindowOptions();
            option.Width = 1400;
            option.WebPreferences = new WebPreferences { 
                WebSecurity = false
            };

            this.emiter = new ContentMainWorkflowEventEmiter();
            Task.Run(async () => {
                var browser = await ElectronNET.API.Electron.WindowManager.CreateWindowAsync(option);
                this.emiter.Initialize();
            }
            );
        }

        private ContentMainWorkflowEventEmiter emiter;
    }
}
