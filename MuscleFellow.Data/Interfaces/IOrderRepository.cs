using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MuscleFellow.Models;

namespace MuscleFellow.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<Guid> AddAsync(Order order);
        Task<Guid> DeleteAsync(Guid orderID);
        Task<int> UpdateAsync(Order order);
        Task<Order> GetAsync(Guid orderID);
        Task<IEnumerable<Order>> GetOrdersAsync(string userID, int pageSize, int pageCount);
    }
}
