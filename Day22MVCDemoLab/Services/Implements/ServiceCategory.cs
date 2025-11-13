using System.Linq.Expressions;

namespace MVCDemoLabpart1.Services.Implements
{
    public class ServiceCategory:IServiceCategory
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceCategory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddCategory(Category category)
        {
            _unitOfWork.CategoriesRepository.Add(category);
            _unitOfWork.Complete();
        }
        public void DeleteCategory(int id)
        {
            var department = _unitOfWork.CategoriesRepository.GetById(id);
            if (department != null)
            {
                _unitOfWork.CategoriesRepository.Delete(id);
                _unitOfWork.Complete();
            }
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categoryList = _unitOfWork.CategoriesRepository.GetAll();
            return categoryList;
        }
        public async Task<Category> GetCategoryByIDAsync(int? id)
        {
            var category = _unitOfWork.CategoriesRepository.GetById(id);
            if (category != null)
            {
                return category;
            }
            else
            {
                return null;
            }
        }
        public int GetCategoryCounter()
        {
            return _unitOfWork.CategoriesRepository.Counter();
        }
        public int GetMaxID()
        {
            return _unitOfWork.CategoriesRepository.GetMaxId();
        }
        public async Task<IEnumerable<Category>> PaginationCategoryAsync(int page = 1, int pageSize = 10)
        {
            var categoryList = _unitOfWork.CategoriesRepository.GetWithPagination(page, pageSize);
            return categoryList;
        }
        public async Task<IEnumerable<Category>> SearchCategoryAsync(Expression<Func<Category, bool>> predicate)
        {
            var searchList = _unitOfWork.CategoriesRepository.Search(predicate);
            return searchList;
        }
        public void UpdateCategory(Category category)
        {
            var oldcategory = _unitOfWork.CategoriesRepository.GetById(category.CategoryId);
            oldcategory.Name = category.Name;
            oldcategory.Description = category.Description;
            _unitOfWork.CategoriesRepository.Update(oldcategory);
            _unitOfWork.Complete();
        }
    }
}
