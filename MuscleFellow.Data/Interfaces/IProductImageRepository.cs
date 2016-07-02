using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Models;

namespace MuscleFellow.Data.Interfaces
{
    public interface IProductImageRepository
    {
        Task<int> AddAsync(ProductImage productImage);
        Task<int> DeleteAsync(int productImageID);
        Task UpdateAsync(ProductImage productImage);
        Task<ProductImage> GetAsync(int productImageID);
        Task<List<ProductImage>> GetProductImages(Guid productID);
    }
}
