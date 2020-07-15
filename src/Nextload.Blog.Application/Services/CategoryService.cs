using Nextload.Blog.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Nextload.Blog.Services
{
    public class CategoryService : BlogAppService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);

            return true;
        }

        public async Task<CategoryDto> GetCategoryDtoByIdAsync(int id)
        {
            var category = await _categoryRepository.FindAsync(id);

            CategoryDto categoryDto = new CategoryDto
            {
                CategoryName = category.CategoryName,
                DisplayName = category.DisplayName
            };

            return categoryDto;

            // return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task<bool> InstertCategoryAsync(CategoryDto categoryDto)
        {
            // var category = ObjectMapper.Map<CategoryDto, Category>(categoryDto);

            Category category = new Category
            {
                CategoryName = categoryDto.CategoryName,
                DisplayName = categoryDto.DisplayName
            };

            var insert = await _categoryRepository.InsertAsync(category);

            return insert != null;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetAsync(id);

            category.CategoryName = categoryDto.CategoryName;

            category.DisplayName = categoryDto.DisplayName;

            var update = await _categoryRepository.UpdateAsync(category);

            return update != null;
        }
    }
}
