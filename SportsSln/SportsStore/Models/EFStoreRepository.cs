using System.Linq;

namespace SportsStore.Models
{
    // the repository implementation just maps the Products property defined by the IStoreRepository interface 
    //onto the Products property defined by the StoreDbContext class

    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
    }
}