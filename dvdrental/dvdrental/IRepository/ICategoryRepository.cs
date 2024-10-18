using dvdrental.Entity;

namespace dvdrental.IRepository
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
    }
}
