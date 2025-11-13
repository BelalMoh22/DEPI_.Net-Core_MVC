using Microsoft.EntityFrameworkCore;

namespace MVCDemoLabpart1.UnitOfWorks.Implements
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly MVCDbContext _context;
        public IRepository<Category> CategoriesRepository { get; }
        public IRepository<Products> ProductsRepository { get; }

        public UnitOfWork(MVCDbContext context,
                          IRepository<Category> categoriesRepository,
                          IRepository<Products> productsRepository)
        {
            _context = context;
            CategoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
            ProductsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        }

        public int Complete()
        {
            var rows = _context.SaveChanges();
            _context.ChangeTracker.Clear();//.State = EntityState.Detached;
            return rows;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
