using System.Threading.Tasks;
using Product.Commands;
using Product.Models;

namespace Product.Services
{
    public interface IProductsService
    {
        Task<PagedSortedList<Product.Models.Product>> GetProducts(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);

        Task<Models.Product> CreateProduct(CreateProductCommand command);

        Task<Models.Product> GetProduct(string productCode);
        
        Task<Models.Product> DeleteProduct(string productCode);
    }
}