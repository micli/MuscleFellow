using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Models;
using MuscleFellow.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MuscleFellow.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private MuscleFellowDbContext _dbContext = null;

        public OrderRepository(MuscleFellowDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Guid> AddAsync(Order order)
        {
            if (null == order)
                return Guid.Empty;
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order.OrderID;
        }

        public async Task<Guid> DeleteAsync(Guid orderID)
        {
            var order = await _dbContext.Orders.SingleOrDefaultAsync(o => o.OrderID == orderID);
            if(null != order)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return orderID;
            }
            return Guid.Empty;
        }
        public async Task<Order> GetAsync(Guid orderID)
        {
            return await _dbContext.Orders.SingleOrDefaultAsync(o => o.OrderID == orderID);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string userID, int pageSize, int pageCount)
        {
            var results = await _dbContext.Orders.Where(o => o.UserID == userID)
                                .Select(o => new { Order = o, })
                                .OrderByDescending(o => o.Order.OrderDate)
                                .Skip(pageSize * pageCount)
                                .Take(pageSize)
                                .ToListAsync();
            return results.Select(o => o.Order);
        }

        public async Task<int> UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
