using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<PagedSortedList<ProductEntity>> Get(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        {
            var query = _dbContext.Products.AsQueryable();

            var total = query.Count();
            var totalPages = (int)Math.Ceiling(total * 1.0 / pageSize);

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortOrder == SortOrder.Desc)
                {
                    query = query.OrderByDescending(sortBy, p => p.Code);
                }
                else
                {
                    query = query.OrderBy(sortBy, p => p.Code);
                    sortOrder = SortOrder.Asc;
                }
            }
            else
            {
                query = query.OrderBy(p => p.Code);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();

            return new PagedSortedList<ProductEntity>()
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder,
                TotalCount = total,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Items = items,
            };
        }

        public async Task<ProductEntity> Create(ProductEntity product)
        {
            await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<ProductEntity> Get(string productCode)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Code == productCode);
        }

        public async Task<ProductEntity> Delete(string productCode)
        {
            var product = await Get(productCode);

            if (product == null)
            {
                return null;
            }

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}