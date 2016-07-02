using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuscleFellow.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MuscleFellow.Models;
using System.Security.Claims;
using System.Security.Principal;
using MuscleFellow.API.Utils;
using MuscleFellow.API.Models;
using Microsoft.Extensions.Options;

namespace MuscleFellow.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly WebApiSettings _settings;
        public ShoppingCartController(ICartItemRepository cartItemRepository, IOptions<WebApiSettings> settings)
        {
            _cartItemRepository = cartItemRepository;
            _settings = settings.Value;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Get([FromQuery] string userID, [FromQuery] int pageSize, [FromQuery] int page)
        {
            if (string.IsNullOrEmpty(userID))
                return NotFound();

            List<CartItem> cartItems = await _cartItemRepository.GetCartItemsAsync(
                string.Empty, userID, pageSize, page);
            foreach(CartItem c in cartItems)
                c.ThumbImagePath = _settings.HostName + c.ThumbImagePath;

            JsonResult result = new JsonResult(cartItems);
            return result;
        }
        [HttpPut]
        // POST api/values
        public async Task<IActionResult> Put([FromBody] CartItem value)
        {
            if (null == value)
                return NoContent();
            CartItem cartItem = await _cartItemRepository.GetByID(value.CartID);
            if (null == cartItem)
                await _cartItemRepository.AddAsync(value);
            else
            {
                cartItem.CreatedDateTime = value.CreatedDateTime;
                cartItem.LastUpdatedDateTime = value.LastUpdatedDateTime;
                cartItem.ProductID = value.ProductID;
                cartItem.ProductName = value.ProductName;
                cartItem.Quantity = value.Quantity;
                cartItem.SessionID = value.SessionID;
                cartItem.SubTotal = value.SubTotal;
                cartItem.ThumbImagePath = value.ThumbImagePath;
                cartItem.UnitPrice = cartItem.UnitPrice;
                
                await _cartItemRepository.UpdateAsync(cartItem);
            }
            return Ok();
        }
        // PUT api/values/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddShoppingCartModel shoppingCartModel)
        {
            IIdentity fedIdentity = GetFederationIdentity(HttpContext.User.Identities);
            if (null == fedIdentity)
                return ResponseHelper.Forbidden();

            // Get current user session id.
            string sessionID = string.Empty;
            if (fedIdentity.IsAuthenticated)
            {
                int count = await _cartItemRepository.AddAsync(
                    sessionID, shoppingCartModel.UserID, 
                    shoppingCartModel.ProductID, shoppingCartModel.Amount);
                if (count > 0)
                    return Ok();
            }

            return ResponseHelper.NotAcceptable();
        }
        // DELETE api/values/5
        [HttpDelete("{cartID}")]
        public async Task<IActionResult> Delete(Guid cartID)
        {
            if (Guid.Empty == cartID)
                return NotFound();
            await _cartItemRepository.DeleteAsync(cartID);
            return Ok();
        }
        private IIdentity GetFederationIdentity(IEnumerable<ClaimsIdentity> Identities)
        {
            var identity = Identities.Where(
                i => i.AuthenticationType == "AuthenticationTypes.Federation").SingleOrDefault();
            return identity;
        }
    }
}
