using Product.Commands;
using Product.Models;

namespace Product.Services
{
    public interface IProductsService
    {
        Task<PagedSortedList<Models.Product>> GetProducts(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);
        Task<Models.Product> GetProduct(string productCode);
        Task<Models.Product> CreateProduct(CreateProductCommand command);
        Task<bool> DeleteProduct(string productCode);
    }
}
