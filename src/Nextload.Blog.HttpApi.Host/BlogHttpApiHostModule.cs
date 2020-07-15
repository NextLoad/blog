using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Nextload.Blog.Services;
using System;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Nextload.Blog
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(BlogHttpApiModule),
        typeof(BlogApplicationModule),
        typeof(BlogSwaggerModule),
        typeof(BlogEntityFrameworkCoreMigrationModule)
    )]
    public class BlogHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(20),
                        ValidateIssuerSigningKey = true,
                        ValidAudience = AppSettings.JwtSettings.Domain,
                        ValidIssuer = AppSettings.JwtSettings.Domain,
                        IssuerSigningKey = new SymmetricSecurityKey(AppSettings.JwtSettings.SecurityKey.GetBytes())
                    };
                });

            context.Services.AddAuthorization();

            context.Services.AddHttpClient();

            context.Services.AddTransient<IAuthorizeService, GithubAuthorzieService>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            // 环境变量，开发环境
            if (env.IsDevelopment())
            {
                // 生成异常页面
                app.UseDeveloperExceptionPage();
            }

            // 路由
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // 路由映射
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}