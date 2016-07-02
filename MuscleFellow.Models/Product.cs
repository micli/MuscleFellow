using System;
using System.Collections.Generic;
namespace MuscleFellow.Models
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string ThumbnailImage { get; set; }
        public List<ProductImage> Images { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string UnitOfLength { get; set; }
        public float Weight { get; set; }
        public string UnitOfWeight { get; set; }
        public float UnitPrice { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
