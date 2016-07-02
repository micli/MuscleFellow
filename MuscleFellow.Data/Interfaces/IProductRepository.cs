using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MuscleFellow.Models;

namespace MuscleFellow.Data.Interfaces
{
    public interface IProductRepository
    {
        // 异步方式获取全部商品，并保存到List<>泛型集合
        Task<List<Product>> GetAllAsync();
        // 异步方式向数据源添加一个商品业务实体信息
        Task<Guid> AddAsync(Product product);
        // 根据给定的商品ID, 异步方式从数据源中删除指定商品的信息
        Task DeleteAsync(Guid productID);
        // 根据给定的商品ID, 异步方式从数据源中提取业务实体
        Task<Product> GetAsync(Guid productID);
        // 根据给定的条件和分页设定，返回符合条件的商品业务实体
        Task<IEnumerable<Product>> GetProductsAsync(string filter, int pageSize, int pageCount);
        // 按照修改时间倒序排列, 返回商品的业务员实体
        Task<IEnumerable<Product>> GetPopularProductsAsync(int count);
        // 异步方式更新指定的商品业务实体信息
        Task<Guid> UpdateAsync(Product product);
    }
}
