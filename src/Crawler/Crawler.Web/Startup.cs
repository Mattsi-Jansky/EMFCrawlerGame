using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Crawler.Web.GameContainers;
using Crawler.Web.WebSocketsControllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Initialises singleton game instance
            var dummy = GameContainer.Instance;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseWebSockets();
        }

        private void OnShutdown()
        {
            GameContainer.Instance.Dispose();
        }
    }
}
