using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Nextload.Blog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<BlogHttpApiHostModule>();

            //Volo.Abp.Modularity.ModuleLoader
            

            //Volo.Abp.Modularity.IModuleLifecycleContributor

            //Volo.Abp.Modularity.ModuleLifecycleContributorBase

            //Volo.Abp.Modularity.OnApplicationInitializationModuleLifecycleContributor
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}