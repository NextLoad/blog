using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nextload.Blog.IServices;
using Nextload.Blog.ToolKits;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Nextload.Blog.Controllers
{
    [Authorize]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_Front)]
    [ApiController]
    [Route("blog")]
    public class PostController : AbpController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            this._postService = postService;
        }

        /// <summary>
        /// 添加博客文章
        /// </summary>
        /// <param name="postDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<string>> InsertPostAsync(PostDto postDto)
        {
            return await _postService.InsertPostAsync(postDto);
        }

        /// <summary>
        /// 获取博客文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult<PostDto>> GetPostByIdAsync(int id)
        {
            return await _postService.GetPostByIdAsync(id);
        }

        /// <summary>
        /// 更新博客信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ServiceResult<string>> UpdatePostAsync(int id, PostDto postDto)
        {
            return await _postService.UpdatePostAsync(id, postDto);
        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ServiceResult> DeletePostAsync(int id)
        {
            return await _postService.DeletePostAsync(id);
        }
    }
}