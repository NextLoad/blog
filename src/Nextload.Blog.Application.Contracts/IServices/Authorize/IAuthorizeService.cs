using Nextload.Blog.ToolKits;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nextload.Blog
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取登录地址
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> GetLoginAddressAsync();

        /// <summary>
        /// 获取Access token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetAccessTokenAsync(string code);


        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(string accessToken);

    }
}
