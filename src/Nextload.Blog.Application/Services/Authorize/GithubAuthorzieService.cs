using IdentityServer4.Stores;
using Meowv.Blog.ToolKits;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Nextload.Blog.ToolKits;
using Nextload.Blog.ToolKits.Github;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Nextload.Blog.Services
{
    public class GithubAuthorzieService : BlogAppService, IAuthorizeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GithubAuthorzieService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<string>> GenerateTokenAsync(string accessToken)
        {
            var result = new ServiceResult<string>();

            if (string.IsNullOrEmpty(accessToken))
            {
                result.IsFail("accessToken为空");
                return result;
            }

            var url = $"{GitHubConfig.ApiUser}?access_token={accessToken}";

            using var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36 Edg/83.0.478.61");

            var httpResponseMessage = await client.GetAsync(url);

            if(httpResponseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                result.IsFail("accessToken 不正确！");
                return result;
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            var user = content.FromJson<UserResponse>();

            if(user == null)
            {
                result.IsFail("未获取到用户数据");
                return result;
            }

            if(user.Id.ToString() != GitHubConfig.UserId)
            {
                result.IsFail("未授权的用户");
                return result;
            }

            var claims = new[]
            {
                //new Claim(ClaimTypes.Name,user.Name?.ToString()),
                //new Claim(ClaimTypes.Email,user.Email?.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(AppSettings.JwtSettings.Expires)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
            };

            var key = new SymmetricSecurityKey(AppSettings.JwtSettings.SecurityKey.SerializeUtf8());
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: AppSettings.JwtSettings.Domain,
                audience: AppSettings.JwtSettings.Domain,
                claims: claims,
                expires: DateTime.Now.AddMinutes(AppSettings.JwtSettings.Expires),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            result.IsSuccess(token);

            return await Task.FromResult(result);
        }

        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ServiceResult<string>();

            if (string.IsNullOrEmpty(code))
            {
                result.IsFail("code不能为空！");
                return result;
            }

            var request = new AccessTokenRequest();

            var content = new StringContent($"code={code}&client_id={request.ClientID}&client_secret={request.ClientSecret}&redirect_uri={request.RedirectUri}");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using var client = _httpClientFactory.CreateClient();

            var httpResponse = await client.PostAsync(GitHubConfig.ApiAccessToken, content);

            var response = await httpResponse.Content.ReadAsStringAsync();

            if (response.StartsWith("access_token"))
                result.IsSuccess(response.Split("=")[1].Split("&").First());
            else
                result.IsFail("code不正确");

            return result;
        }

        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            var result = new ServiceResult<string>();

            var request = new AuthorizeRequest();

            var address = string.Concat(new string[]
            {
                GitHubConfig.ApiAuthorize,
                "?client_id=",request.ClientID,
                "&scope=",request.Scope,
                "&state=",request.State,
                "&redirect_uri=",request.RedirectUri
            });

            result.IsSuccess(address);

            return await Task.FromResult(result);
        }
    }
}