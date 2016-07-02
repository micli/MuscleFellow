using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Models;
namespace MuscleFellow.Data.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<int> AddAsync(OrderDetail order);
        Task<int> DeleteAsync(int orderDetailID);
        Task<int> UpdateAsync(OrderDetail orderDetail);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync(Guid orderID, int pageSize, int pageCount);
    }
}
