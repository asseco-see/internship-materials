using Microsoft.EntityFrameworkCore;
using Product.Database.Entities;

namespace Product.Database.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("products");

            builder.HasKey(x => x.Code);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Kind).HasConversion<string>().IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Description).HasMaxLength(1024);
            builder.Property(x => x.Status).HasConversion<string>();
            builder.Property(x => x.AvailabilityStart);
            builder.Property(x => x.AvailabilityEnd);
            builder.Property(x => x.IsPackage);
        }
    }
}
