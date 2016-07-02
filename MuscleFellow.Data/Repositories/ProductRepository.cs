using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Data.Interfaces;
using MuscleFellow.Models;
using Microsoft.EntityFrameworkCore;

namespace MuscleFellow.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private MuscleFellowDbContext _dbContext = null;
        public ProductRepository(MuscleFellowDbContext dbContenxt)
        {
            _dbContext = dbContenxt;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<Guid> AddAsync(Product product)
        {
            if (null == product)
                return Guid.Empty;
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.ProductID;
        }
        public async Task DeleteAsync(Guid productID)
        {
            var product = await _dbContext.Products
                .SingleOrDefaultAsync(p => p.ProductID == productID);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<Product> GetAsync(Guid productID)
        {
            return await _dbContext.Products.Where(p => p.ProductID == productID).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(string filter, int pageSize, int pageCount)
        {
            var results = await _dbContext.Products.Where
                    (p => (String.IsNullOrEmpty(filter) || 
                     p.ProductName.Contains(filter) || p.Description.Contains(filter)))
                    .Select(p => new { Product = p, })
                    .Skip(pageSize * pageCount)
                    .Take(pageSize)
                    .ToListAsync();

            return results.Select(p => p.Product);
        }
        public async Task<IEnumerable<Product>> GetPopularProductsAsync(int count)
        {
            var results = await _dbContext.Products
                    .Select(p => new { Product = p, })
                    .Take(count)
                    .ToListAsync();

            return results.Select(p => p.Product);
        }
        public async Task<Guid> UpdateAsync(Product product)
        {
            if (null == product)
                return Guid.Empty;
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product.ProductID;
        }
    }
}
