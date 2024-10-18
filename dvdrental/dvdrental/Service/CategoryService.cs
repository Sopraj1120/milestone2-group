using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;

namespace dvdrental.Service
{

    public class CategoryService :ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponceDto> AddCategory(CategoryRequestDto categoryDto)
        {

            var category = new Category
            {
                Name = categoryDto.Name
            };


            Category category1 = category;
            await _categoryRepository.AddCategory(category1);


            return new CategoryResponceDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public async Task UpdateCategory(int id, CategoryRequestDto categoryDto)
        {

            var existingCategory = await _categoryRepository.GetCategoryById(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }


            existingCategory.Name = categoryDto.Name;


            await _categoryRepository.UpdateCategory(existingCategory);
        }
        public async Task DeleteCategory(int id)
        {

            var existingCategory = await _categoryRepository.GetCategoryById(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }


            await _categoryRepository.DeleteCategory(id);
        }

        public async Task<List<CategoryResponceDto>> GetCategories()
        {

            var categories = await _categoryRepository.GetCategories();


            return categories.Select(c => new CategoryResponceDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }


        public async Task<CategoryResponceDto> GetCategoryById(int id)
        {

            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }


            return new CategoryResponceDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

    }
}
