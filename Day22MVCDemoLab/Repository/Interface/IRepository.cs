using System.Linq.Expressions;

namespace MVCDemoLabpart1.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        int Counter();
        IEnumerable<T> GetAllIncluding(params string[] includes);
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetWithPagination(int page = 1, int pageSize = 10);
        int GetMaxId();
    }
}
