using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Data.Interfaces;
using MuscleFellow.Models;
using Microsoft.EntityFrameworkCore;

namespace MuscleFellow.Data.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private MuscleFellowDbContext _dbContext = null;
        public ProductImageRepository(MuscleFellowDbContext context)
        {
            _dbContext = context;
        }
        public async Task<int> AddAsync(ProductImage productImage)
        {
            if (null == productImage)
                return -1;
            _dbContext.ProductImages.Add(productImage);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int productImageID)
        {
            var pi = _dbContext.ProductImages.SingleOrDefault(i => i.ImageID == productImageID);
            if (null != pi)
            {
                _dbContext.ProductImages.Remove(pi);
                return await _dbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<ProductImage> GetAsync(int productImageID)
        {
            return await _dbContext.ProductImages
                .Where(i => i.ImageID == productImageID).SingleOrDefaultAsync();
        }

        public async Task<List<ProductImage>> GetProductImages(Guid productID)
        {
            var results = await _dbContext.ProductImages.Where
                (i => i.ProductID == productID)
                .Select(i => new { ProductImage = i, })
                .ToListAsync();

            return results.Select(i => i.ProductImage).ToList();
        }

        public async Task UpdateAsync(ProductImage productImage)
        {
            _dbContext.ProductImages.Update(productImage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
