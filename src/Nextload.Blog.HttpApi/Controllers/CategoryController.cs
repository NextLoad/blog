using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Nextload.Blog.Controllers
{
    [Authorize]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_Front)]
    [ApiController]
    [Route("category")]
    public class CategoryController : AbpController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        /// <summary>
        /// 插入目录
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> InsertCategoryAsync(CategoryDto categoryDto)
        {
            return await _categoryService.InstertCategoryAsync(categoryDto);
        }

        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            return await _categoryService.UpdateCategoryAsync(id, categoryDto);
        }

        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            return await _categoryService.GetCategoryDtoByIdAsync(id);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryService.DeleteCategoryAsync(id);
        }
    }
}