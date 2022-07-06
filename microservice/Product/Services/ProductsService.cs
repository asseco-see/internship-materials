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
        public async Task<Models.Product> CreateProduct(CreateProductCommand command)
        {
            var entity = _mapper.Map<ProductEntity>(command);

            var existingProduct = await _productsRepository.Get(command.ProductCode);
            if (existingProduct != null)
            {
                return null;
            }
            var result = await _productsRepository.Create(entity);

            return _mapper.Map<Models.Product>(result);
        }

        public async Task<bool> DeleteProduct(string productCode)
        {
            return await _productsRepository.Delete(productCode);
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

        public async Task<PagedSortedList<Models.Product>> GetProducts(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        {
            var result = await _productsRepository.List(page, pageSize, sortBy, sortOrder);

            return _mapper.Map<PagedSortedList<Models.Product>>(result);
        }
    }
}
