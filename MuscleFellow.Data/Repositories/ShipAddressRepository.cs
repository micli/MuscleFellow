using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Models;
using MuscleFellow.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuscleFellow.Models.BasicInfo;

namespace MuscleFellow.Data.Repositories
{
    public class ShipAddressRepository : IShipAddressRepository
    {
        private readonly MuscleFellowDbContext _dbContext;
        public ShipAddressRepository(MuscleFellowDbContext context)
        {
            _dbContext = context;
        }
        public async Task<int> AddAsync(ShipAddress address)
        {
            _dbContext.ShipAddress.Add(address);
            await _dbContext.SaveChangesAsync();
            return address.AddressID;
        }
        public async Task<int> DeleteAsync(int addressID)
        {
            var address = _dbContext.ShipAddress.SingleOrDefault(a => a.AddressID == addressID);
            if (null != address)
            {
                _dbContext.Remove(address);
                return await _dbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<List<ShipAddress>> GetAddressAsync(string userID, int pageSize, int pageCount)
        {
            var results = await _dbContext.ShipAddress.Where(a => a.UserID == userID)
                    .Select(a => new { ShipAddress = a, })
                    .Skip(pageSize * pageCount)
                    .Take(pageSize)
                    .ToListAsync();
            return results.Select(a => a.ShipAddress).ToList();
        }
        public async Task<ShipAddress> GetAsync(int id)
        {
            if (0 >= id)
                return null;
            return await _dbContext.ShipAddress.FirstOrDefaultAsync(a => a.AddressID == id);
        }
        public async Task<int> UpdateAsync(ShipAddress address)
        {
            if (null == address)
                return -1;
            _dbContext.ShipAddress.Update(address);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Province>> GetProvincesAsync()
        {
            return  await _dbContext.Provinces.ToListAsync();
        }
        public async Task<List<City>> GetCitiesAsync(int provinceID)
        {
            return await _dbContext.Cities.Where(a => a.ProvinceID == provinceID).ToListAsync();
        }
        public async Task<string> GetProvinceNameAsync(int provinceID)
        {
            Province province = await _dbContext.Provinces.SingleOrDefaultAsync(p => p.ID == provinceID);
            if (null == province)
                return string.Empty;
            else
                return province.Name;
        }
        public async Task<string> GetCityNameAsync(int cityID)
        {
            City city = await _dbContext.Cities.SingleOrDefaultAsync(c => c.ID == cityID);
            if (null == city)
                return string.Empty;
            else
                return city.Name;
        }
    }
}
