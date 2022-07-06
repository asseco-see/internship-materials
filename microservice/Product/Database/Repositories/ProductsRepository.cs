using Microsoft.EntityFrameworkCore;
using Product.Database.Entities;
using Product.Models;

namespace Product.Database.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsDbContext _dbContext;

        public ProductsRepository(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductEntity> Create(ProductEntity product)
        {
            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<bool> Delete(string productCode)
        {
            var product = await Get(productCode);

            if (product == null)
            {
                return false;
            }

            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProductEntity> Get(string productCode)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Code == productCode);
        }

        public async Task<PagedSortedList<ProductEntity>> List(int page = 1, int pageSize = 5, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        {
            var query = _dbContext.Products.AsQueryable();

            var totalCount = query.Count();

            var totalPages = (int)Math.Ceiling(totalCount * 1.0 / pageSize);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "code":
                        query = sortOrder == SortOrder.Asc ? query.OrderBy(x => x.Code) : query.OrderByDescending(x => x.Code);
                        break;
                    case "description":
                        query = sortOrder == SortOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description);
                        break;
                    default:
                    case "name":
                        query = sortOrder == SortOrder.Asc ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                        break;
                }
            } 
            else
            {
                query = query.OrderBy(p => p.Name);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();

            return new PagedSortedList<ProductEntity>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = items,
                SortBy = sortBy,
                SortOrder = sortOrder
            };
        }
    }
}
