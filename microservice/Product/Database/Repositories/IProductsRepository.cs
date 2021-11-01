using System.Threading.Tasks;
using Product.Database.Entities;
using Product.Models;

namespace Product.Database.Repositories
{
    public interface IProductsRepository
    {
        Task<PagedSortedList<ProductEntity>> Get(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);

        Task<ProductEntity> Create(ProductEntity product);

        Task<ProductEntity> Get(string productCode);
        
        Task<ProductEntity> Delete(string productCode);
    }
}