using System.Linq.Expressions;

namespace MVCDemoLabpart1.Services.Interface
{
    public interface IServiceProduct
    {
        //Logical Business
        IEnumerable<Products> GetProducts();
        Products GetProductByID(int id);
        void AddProduct(Products product);
        void UpdateProduct(Products product);
        void DeleteProduct(int id);
        int GetProductCounter();
        IEnumerable<Products> PaginationProduct(int page = 1, int pageSize = 10);
        IEnumerable<Products> SearchProduct(Expression<Func<Products, bool>> predicate);
        int GetMaxID();
    }
}
