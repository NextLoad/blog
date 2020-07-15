using System;
using System.Collections.Generic;
using System.Text;

namespace Nextload.Blog
{
    public class GitHubConfig
    {
        public const string ApiAuthorize = "https://github.com/login/oauth/authorize";

        public const string ApiAccessToken = "https://github.com/login/oauth/access_token";

        public const string ApiUser = "https://api.github.com/user";

        public static string UserId = AppSettings.GitHubSettings.UserId;

        public static string ClientId = AppSettings.GitHubSettings.ClientID;

        public static string ClientSecret = AppSettings.GitHubSettings.ClientSecret;

        public static string RedirectUri = AppSettings.GitHubSettings.RedirectUri;

        public static string ApplicationName = AppSettings.GitHubSettings.ApplicationName;
    }
}
