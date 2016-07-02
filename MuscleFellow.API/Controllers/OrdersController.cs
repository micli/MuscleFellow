using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MuscleFellow.Models;
using MuscleFellow.Data.Interfaces;
using MuscleFellow.API.Utils;
using MuscleFellow.API.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MuscleFellow.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShipAddressRepository _shipAddressRepository;
        public OrdersController(IOrderRepository orderRepository, 
            IOrderDetailRepository orderDetailRepository, 
            IProductRepository productRepository, IShipAddressRepository shipAddressRepostitory)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _shipAddressRepository = shipAddressRepostitory;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string userID, [FromQuery] int PageSize, [FromQuery] int page)
        {
            if (string.IsNullOrEmpty(userID))
                return NotFound();
            IEnumerable<Order> orderList =  await _orderRepository.GetOrdersAsync(userID, PageSize, page);
            JsonResult result = new JsonResult(orderList);
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddShoppingCartModel model)
        {
            if (string.IsNullOrEmpty(model.UserID) 
                || Guid.Empty == model.ProductID || 0 >= model.Amount)
                return ResponseHelper.NotAcceptable();
            var product = await _productRepository.GetAsync(model.ProductID);
            if (null == product)
                return NotFound();

            List<ShipAddress> addressList = await _shipAddressRepository.GetAddressAsync(model.UserID, 50, 0);
            Order order = new Order()
            {
                Address = addressList.Count > 0 ? addressList[0].ToString() + "(mobile)" : string.Empty + "(mobile)",
                OrderID = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.PendingPayment,
                UserID = model.UserID,
            };

            OrderDetail orderDetail = new OrderDetail()
            {
                OrderID = order.OrderID,
                PlaceDate = DateTime.Now,
                ProductName = product.ProductName,
                ProductID = product.ProductID,
                ThumbImagePath = product.ThumbnailImage,
                UnitPrice = product.UnitPrice,
                Quantity = model.Amount,
                SubTotal = product.UnitPrice * model.Amount
            };
            order.TotalPrice = orderDetail.SubTotal;

            Guid orderID = await _orderRepository.AddAsync(order);
            // Add order failed.
            if (orderID == Guid.Empty)
                return ResponseHelper.InternalServerError();
           
             int count = await _orderDetailRepository.AddAsync(orderDetail);
            if (0 < count)
                return Ok();
            else
                return ResponseHelper.InternalServerError();
        }
    }
}
