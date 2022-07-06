using Microsoft.EntityFrameworkCore;
using Product.Database.Entities;
using System.Reflection;

namespace Product.Database
{
    public class ProductsDbContext: DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ProductsDbContext()
        {

        }

        public ProductsDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
