using Product.Database.Entities;

namespace Product.Models
{
    public class Product
    {
        public string ProductCode { get; set; }

        public ProductKind Kind { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProductStatus Status { get; set; }

        public bool IsPackage { get; set; }

        public string ImageUrl { get; set; }

        public DateTime AvailabilityStart { get; set; }

        public DateTime AvailabilityEnd { get; set; }
    }
}
