using System.Linq.Expressions;

namespace MVCDemoLabpart1.Services.Interface
{
    public interface IServiceCategory
    {
        //Logical Business
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIDAsync(int? id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        int GetCategoryCounter();
        Task<IEnumerable<Category>> PaginationCategoryAsync(int page = 1, int pageSize = 10);
        Task<IEnumerable<Category>> SearchCategoryAsync(Expression<Func<Category, bool>> predicate);
        int GetMaxID();
    }
}
