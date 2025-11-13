using MVCDemoLabpart1.Models;
using MVCDemoLabpart1.Repository.Interface;

namespace MVCDemoLabpart1.UnitOfWorks.Inferface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> CategoriesRepository { get; }
        IRepository<Products> ProductsRepository { get; }
        int Complete();
    }
}
