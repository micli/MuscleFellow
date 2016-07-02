using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuscleFellow.Data.Interfaces;
using MuscleFellow.Models;
using MuscleFellow.Models.BasicInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using MuscleFellow.Web.Models.ShipAddress;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MuscleFellow.Web.Controllers
{

    public class ShipAddressController : Controller
    {
        private readonly IShipAddressRepository _addressRepository;
        private readonly int _MaxAddressCount = 10;
        public ShipAddressController(IShipAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;    
        }
        [Authorize]
        // GET: ShipAddress
        public async Task<IActionResult> Index()
        {
            string userID = HttpContext.User.Identity.Name;
            List<ShipAddress> AddressList = await _addressRepository.GetAddressAsync(userID, _MaxAddressCount, 0);
            return View(AddressList);
        }
        [Authorize]
        // GET: ShipAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddress = await _addressRepository.GetAsync((int) id);
            if (shipAddress == null)
            {
                return NotFound();
            }

            return View(shipAddress);
        }
        [Authorize]
        // GET: ShipAddresses/Create
        public async Task<IActionResult> Create()
        {
            AddressCreationModel acm = new AddressCreationModel();
            acm.Provinces = new List<SelectListItem>();
            acm.Cities = new List<SelectListItem>();
            acm.ProvinceListName = "Province";
            List<Province> pList = await _addressRepository.GetProvincesAsync();
            foreach (Province p in pList)
                acm.Provinces.Add(new SelectListItem { Text = p.Name, Value = p.ID.ToString() });
            acm.CityListName = "City";
            return View(acm);
        }
        [Authorize]
        // POST: ShipAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressID,Address,City,PhoneNumber,Province,Receiver,UserID,ZipCode")] ShipAddress shipAddress)
        {
            if (ModelState.IsValid)
            {
                string temp = string.Empty;
                temp = Request.Form["dpProvince"].ToString();
                int provinceID = 0;
                int cityID = 0;

                int.TryParse(temp, out provinceID);
                shipAddress.Province = await _addressRepository.GetProvinceNameAsync(provinceID);
                temp = Request.Form["dpProvince"].ToString();
                int.TryParse(temp, out cityID);
                shipAddress.City = await _addressRepository.GetCityNameAsync(cityID);
                shipAddress.UserID = HttpContext.User.Identity.Name;
                int count = await _addressRepository.AddAsync(shipAddress);
                return RedirectToAction("Index");
            }
            return View(shipAddress);
        }
        [Authorize]
        // GET: ShipAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var shipAddress = await _addressRepository.GetAsync((int)id);
            if (shipAddress == null)
            {
                return NotFound();
            }
            return View(shipAddress);
        }
        [Authorize]
        // POST: ShipAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressID,Address,City,PhoneNumber,Province,Receiver,UserID,ZipCode")] ShipAddress shipAddress)
        {
            if (id != shipAddress.AddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _addressRepository.UpdateAsync(shipAddress);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipAddressExists(shipAddress.AddressID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(shipAddress);
        }
        [Authorize]
        // GET: ShipAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddress = await _addressRepository.GetAsync((int) id);
            if (shipAddress == null)
            {
                return NotFound();
            }

            return View(shipAddress);
        }
        [Authorize]
        // POST: ShipAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _addressRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<List<Province>> GetProvince()
        {
            return await _addressRepository.GetProvincesAsync();
        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> GetCities(int provinceID)
        {
            List<City> cities = await _addressRepository.GetCitiesAsync(provinceID);
            JsonResult result = new JsonResult(cities);
            return result;
        }
        private bool ShipAddressExists(int id)
        {
            if (_addressRepository.GetAsync(id) != null)
                return true;
            else
                return false;
        }
    }
}
