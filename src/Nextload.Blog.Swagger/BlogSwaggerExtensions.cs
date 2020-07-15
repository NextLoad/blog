using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nextload.Blog
{
    public static class BlogSwaggerExtensions
    {
        internal class SwaggerApiInfo
        {
            /// <summary>
            /// url前缀
            /// </summary>
            public string UrlPrefix { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            public OpenApiInfo OpenApiInfo { get; set; }
        }


        private static readonly List<SwaggerApiInfo> _apiInfos = new List<SwaggerApiInfo>
        {
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_Front,
                Name = "博客前台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "nextload - 博客前台接口",
                    Description = "博客前台接口描述"
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_Back,
                Name = "博客后台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "nextload - 博客后台接口",
                    Description = "博客后台接口描述"
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_Common,
                Name = "通用公共接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "nextload - 通用公共接口",
                    Description = "通用公共接口描述"
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_Auth,
                Name = "JWT认证授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "nextload - JWT认证授权接口",
                    Description = "JWT认证授权接口描述"
                }
            }
        };

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                //options.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "1.0.0",
                //    Title = "我的接口",
                //    Description = "接口描述"
                //});

                //options.SwaggerDoc("v2", new OpenApiInfo { 
                //    Version = "2.0.0",
                //    Title = "我的接口2",
                //    Description = "接口描述2"
                //});

                _apiInfos.ForEach(a => options.SwaggerDoc(a.UrlPrefix, a.OpenApiInfo));

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Nextload.Blog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Nextload.Blog.Application.Contracts.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Nextload.Blog.HttpApi.xml"));

                //options.DocumentFilter<SwaggerDocumentFilter>();

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                };

                options.AddSecurityDefinition("oauth2", security);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       security, new List<string>()
                    }
                });

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        }, new List<string>()
                //    }
                //});
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "默认接口");

            //    options.SwaggerEndpoint("/swagger/v2/swagger.json", "默认接口2");
            //});

            app.UseSwaggerUI(options =>
            {
                _apiInfos.ForEach(a => options.SwaggerEndpoint($"/swagger/{a.UrlPrefix}/swagger.json", a.Name));

                options.DefaultModelExpandDepth(-1);

                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);

                // 设置为空可以直接输入地址 而不需要像这样输入 http://localhost:5000/swagger/index.html
                //options.RoutePrefix = string.Empty;

                options.DocumentTitle = "Nextload - 接口";
            });
        }
    }
}