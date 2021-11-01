using System.Threading.Tasks;
using AutoMapper;
using Product.Commands;
using Product.Database.Entities;
using Product.Database.Repositories;
using Product.Models;

namespace Product.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsService(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<PagedSortedList<Models.Product>> GetProducts(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        {
            var list = await _productsRepository.Get(page, pageSize, sortBy, sortOrder);

            return _mapper.Map<PagedSortedList<Models.Product>>(list);
        }

        public async Task<Models.Product> CreateProduct(CreateProductCommand command)
        {
            var productToAdd = _mapper.Map<ProductEntity>(command);

            await _productsRepository.Create(productToAdd);

            return _mapper.Map<Models.Product>(productToAdd);
        }

        public async Task<Models.Product> GetProduct(string productCode)
        {
            var productEntity = await _productsRepository.Get(productCode);

            if (productEntity == null)
            {
                return null;
            }

            return _mapper.Map<Models.Product>(productEntity);
        }

        public async Task<Models.Product> DeleteProduct(string productCode)
        {
            var deletedProduct = await _productsRepository.Delete(productCode);

            if (deletedProduct == null)
            {
                return null;
            }

            return _mapper.Map<Models.Product>(deletedProduct);
        }
    }
}