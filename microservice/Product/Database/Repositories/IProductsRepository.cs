using Product.Database.Entities;
using Product.Models;

namespace Product.Database.Repositories
{
    public interface IProductsRepository
    {
        Task<PagedSortedList<ProductEntity>> List(int page = 1, int pageSize = 5, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);
        
        Task<ProductEntity> Create(ProductEntity product);

        Task<ProductEntity> Get(string productCode);

        Task<bool> Delete(string productCode);
    }
}
