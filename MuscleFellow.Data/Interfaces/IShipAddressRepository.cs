using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MuscleFellow.Models;
using MuscleFellow.Models.BasicInfo;

namespace MuscleFellow.Data.Interfaces
{
    public interface IShipAddressRepository
    {
        Task<int> AddAsync(ShipAddress address);
        Task<int> DeleteAsync(int addressID);
        Task<List<ShipAddress>> GetAddressAsync(string userID, int pageSize, int pageCount);
        Task<ShipAddress> GetAsync(int id);
        Task<int> UpdateAsync(ShipAddress address);
        Task<List<Province>> GetProvincesAsync();
        Task<List<City>> GetCitiesAsync(int provinceID);
        Task<string> GetProvinceNameAsync(int provinceID);
        Task<string> GetCityNameAsync(int cityID);
    }
}
