using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nextload.Blog.ToolKits;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Nextload.Blog.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("auth")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_Auth)]
    public class AuthController : AbpController
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthController(IAuthorizeService authorizeService)
        {
            this._authorizeService = authorizeService;
        }

        [HttpGet("url")]
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeService.GetLoginAddressAsync();
        }

        [HttpGet("accesstoken")]
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            return await _authorizeService.GetAccessTokenAsync(code);
        }

        [HttpGet("token")]
        public async Task<ServiceResult<string>> GetJWTTokenAsync(string accessToken)
        {
            return await _authorizeService.GenerateTokenAsync(accessToken);
        }
    }
}