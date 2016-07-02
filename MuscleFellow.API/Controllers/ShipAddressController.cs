using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuscleFellow.Data.Interfaces;
using MuscleFellow.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MuscleFellow.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ShipAddressController : Controller
    {
        private readonly IShipAddressRepository _shipAddressRepository;
        public ShipAddressController(IShipAddressRepository shipAddressRepository)
        {
            _shipAddressRepository = shipAddressRepository;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userID = HttpContext.User.Identity.Name;
            List<ShipAddress> addressList = await _shipAddressRepository.GetAddressAsync(userID, 0, 50);
            JsonResult result = new JsonResult(addressList);
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            string userID = HttpContext.User.Identity.Name;
            ShipAddress address = await _shipAddressRepository.GetAsync(id);
            JsonResult result = new JsonResult(address);
            return result;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShipAddress value)
        {
            string userID = HttpContext.User.Identity.Name;
            int count = await _shipAddressRepository.AddAsync(value);
            return Ok(value.AddressID);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
