using System;
using System.ComponentModel.DataAnnotations;
using Product.Database.Entities;

namespace Product.Commands
{
    public class CreateProductCommand
    {
        [Required]
        public string ProductCode { get; set; }

        [Required]
        public ProductKind? Kind { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProductStatus? Status { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? AvailabilityStart { get; set; }

        public DateTime? AvailabilityEnd { get; set; }
    }
}