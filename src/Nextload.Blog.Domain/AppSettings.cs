using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nextload.Blog
{
    public class AppSettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        /// <summary>
        /// Constructor
        /// </summary>
        static AppSettings()
        {
            // 加载appsettings.json，并构建IConfigurationRoot
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        /// <summary>
        /// EnableDb
        /// </summary>
        public static string EnableDb => _config["ConnectionStrings:Enable"];

        /// <summary>
        /// ConnectionStrings
        /// </summary>
        public static string ConnectionStrings => _config.GetConnectionString(EnableDb);

        /// <summary>
        /// apiversion
        /// </summary>
        public static string ApiVersion => _config["ApiVersion"];

        public static class GitHubSettings
        {
            public static string UserId => _config["Github:UserId"];

            public static string ClientID => _config["Github:ClientID"];

            public static string ClientSecret => _config["Github:ClientSecret"];

            public static string RedirectUri => _config["Github:RedirectUri"];

            public static string ApplicationName => _config["Github:ApplicationName"];
        }

        public static class JwtSettings
        {
            public static string Domain => _config["JWT:Domain"];
            public static string SecurityKey => _config["JWT:SecurityKey"];
            public static double Expires => Convert.ToDouble(_config["JWT:Expires"]);
        }
    }
}
