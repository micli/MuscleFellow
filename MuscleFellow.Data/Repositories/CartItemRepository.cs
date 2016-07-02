using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Models;
using MuscleFellow.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MuscleFellow.Data.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private MuscleFellowDbContext _dbContext = null;
        private readonly int _MaxCartItemCount = 30;
        public CartItemRepository(MuscleFellowDbContext context)
        {
            _dbContext = context;
        }

        public async Task<int> AddAsync(CartItem cartItem)
        {
            if (cartItem.UserID == "anonymous")
            {
                if (_dbContext.CartItems.Count(c => c.SessionID == cartItem.SessionID) > _MaxCartItemCount)
                    return 0;
            }
            else
            {
                if (_dbContext.CartItems.Count(c => c.UserID == cartItem.UserID) > _MaxCartItemCount)
                    return 0;
            }
            CartItem existedCartItem = null;
            if (cartItem.UserID == "anonymous")
                existedCartItem = await _dbContext.CartItems.FirstOrDefaultAsync(
                    c => c.SessionID == cartItem.SessionID && c.ProductID == cartItem.ProductID);
            else
                existedCartItem = await _dbContext.CartItems.FirstOrDefaultAsync(
                    c => c.UserID == cartItem.UserID && c.ProductID == cartItem.ProductID);
            if(null != existedCartItem)
            {
                existedCartItem.Quantity += cartItem.Quantity;
                existedCartItem.ProductName = cartItem.ProductName;
                existedCartItem.ThumbImagePath = cartItem.ThumbImagePath;
                existedCartItem.UnitPrice = cartItem.UnitPrice;
                existedCartItem.SubTotal = existedCartItem.Quantity * existedCartItem.UnitPrice;
                _dbContext.CartItems.Update(existedCartItem);
            }
            else
                _dbContext.CartItems.Add(cartItem);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> AddAsync(string sessionID, string userID, Guid productID, int amount)
        {
            Product prod = await _dbContext.Products.Where(
                p => p.ProductID == productID).SingleAsync();
            if (null == prod)
                return 0;
            CartItem item = new CartItem();
            item.CartID = Guid.NewGuid();
            item.UserID = userID;
            item.ProductID = productID;
            item.ProductName = prod.ProductName;
            item.ThumbImagePath = prod.ThumbnailImage;
            item.SessionID = sessionID;
            item.CreatedDateTime = DateTime.Now;
            item.LastUpdatedDateTime = item.CreatedDateTime;
            item.Quantity = amount;
            item.UnitPrice = prod.UnitPrice;
            item.SubTotal = prod.UnitPrice * amount;

            return await AddAsync(item);
        }
        public async Task<int> DeleteAsync(Guid cartItemID)
        {
            var cartItem = await _dbContext.CartItems.SingleOrDefaultAsync(c => c.CartID == cartItemID);
            if (null != cartItem)
            {
                _dbContext.CartItems.Remove(cartItem);
                return await _dbContext.SaveChangesAsync();       
            }
            return 0;
        }
        public async Task<int> UpdateAnonymousCartItem(string sessionID, string userID)
        {
            if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(sessionID))
                return 0;
            List<CartItem> items = _dbContext.CartItems.Where(c => c.SessionID == sessionID).ToList();
            foreach (CartItem item in items)
            {
                item.UserID = userID;
                _dbContext.CartItems.Update(item);
            }
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<List<CartItem>> GetCartItemsAsync(string sessionID, string userID, int pageSize, int pageCount)
        {
            var results = await _dbContext.CartItems.Where(c => c.UserID == userID || c.SessionID == sessionID)
                   .Select(c => new { CartItem = c, })
                   .OrderByDescending(c => c.CartItem.CreatedDateTime)
                   .Skip(pageSize * pageCount)
                   .Take(pageSize)
                   .ToListAsync();
            return results.Select(c => c.CartItem).ToList();
        }
        public async Task<List<CartItem>> GetCartItemsAsync(string sessionID, int pageSize, int pageCount)
        {
            var results = await _dbContext.CartItems.Where(c => c.SessionID == sessionID)
                   .Select(c => new { CartItem = c, })
                   .OrderByDescending(c => c.CartItem.CreatedDateTime)
                   .Skip(pageSize * pageCount)
                   .Take(pageSize)
                   .ToListAsync();
            return results.Select(c => c.CartItem).ToList();
        }
        public async Task<int> UpdateAsync(CartItem cartItem)
        {
            if (null == cartItem)
                return -1;
            _dbContext.CartItems.Update(cartItem);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<CartItem> GetByID(Guid id)
        {
            return await _dbContext.CartItems.FirstOrDefaultAsync(c => c.CartID == id);
        }
    }
}
