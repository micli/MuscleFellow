using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuscleFellow.Data;
using MuscleFellow.Models;
using System;
using System.Threading.Tasks;
using MuscleFellow.Data.Interfaces;
using System.Collections.Generic;

namespace MuscleFellow.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly int _MaxCartItemCount = 30;
        public ShoppingCartController(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddToShoppingCart([FromQuery] Guid productID, [FromQuery] int amount)
        {
            // Get current user session id.
            string sessionID = HttpContext.Session.Id;
            // Get current user name
            string userID = "anonymous";
            if (HttpContext.User.Identity.IsAuthenticated)
                userID = HttpContext.User.Identity.Name;

            int count = await _cartItemRepository.AddAsync(sessionID, userID, productID, amount);
            JsonResult result = null;
            if (count > 0)
                result = new JsonResult(
                    new
                    {
                        success = true,
                        message = "商品已经被添加进<a href=\"/ShoppingCart\">购物车</a>",
                    });
            else
                result = new JsonResult(
                    new
                    {
                        success = true,
                        message = "添加购物车失败，请重试",
                    });
            return result;
        }
        // GET: ShoppingCartController
        public async Task<IActionResult> Index()
        {
            string sessionID = HttpContext.Session.Id;
            List<CartItem> cartList = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // Get current user name
                string userID = HttpContext.User.Identity.Name;
                cartList = await _cartItemRepository.GetCartItemsAsync(sessionID, userID, _MaxCartItemCount, 0);
            }
            else
                cartList = await _cartItemRepository.GetCartItemsAsync(sessionID, _MaxCartItemCount, 0);
            return View(cartList);
        }
        // GET: ShoppingCartController/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // GET: ShoppingCartController/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int count = await _cartItemRepository.DeleteAsync((Guid)id);
            return RedirectToAction("Index");
        }
    }
}