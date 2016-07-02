using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuscleFellow.Data;
using MuscleFellow.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MuscleFellow.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MuscleFellow.Web.Models.Orders;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MuscleFellow.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly int _MaxAddressCount = 10;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IShipAddressRepository _shipAddressRepository;

        public OrdersController(IOrderRepository orderRepository, 
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            ICartItemRepository cartItemRepository,
            IShipAddressRepository shipAddressRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _cartItemRepository = cartItemRepository;
            _shipAddressRepository = shipAddressRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Confirm([FromQuery] string idList, [FromQuery] string ProductID, [FromQuery] int Amount)
        {
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(ProductID))
                return Redirect("/Home");

            string userID = HttpContext.User.Identity.Name;
            OrderConfirmModel orderConfirmModel = new OrderConfirmModel();
            orderConfirmModel.ShipAddresses = new List<SelectListItem>();
            orderConfirmModel.OrderItem = new Order()
            {
                OrderID = Guid.NewGuid(),
                UserID = userID,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.PendingPayment
            };


            Guid tempGuid = Guid.Empty;
            List<ShipAddress> addressStringList = await _shipAddressRepository.GetAddressAsync(userID, _MaxAddressCount, 0);
            foreach (ShipAddress sa in addressStringList)
                orderConfirmModel.ShipAddresses.Add(
                    new SelectListItem { Text = sa.ToString(), Value = sa.ToString() });
            if(string.IsNullOrEmpty(idList))
            {
                orderConfirmModel.OrderDetails = await CreateOrderDetailsByProductAsync(
                    orderConfirmModel.OrderItem.OrderID, ProductID, Amount);
            }
            else
            {
                orderConfirmModel.OrderDetails = await CreateOrderDetailsByCartIDListAsync(
                    orderConfirmModel.OrderItem.OrderID, idList);
            }
            orderConfirmModel.IDList = idList;
            orderConfirmModel.ProductID = ProductID;
            orderConfirmModel.ProductAmount = Amount;
            foreach(OrderDetail od in orderConfirmModel.OrderDetails)
                orderConfirmModel.TotalAmount += od.SubTotal;

            return View(orderConfirmModel);
        }
        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            string idList = HttpContext.Request.Form["IDListCtrl"].ToString();
            string productID = HttpContext.Request.Form["prodIdCtrl"].ToString();
            int amount = int.Parse(HttpContext.Request.Form["amountCtrl"].ToString());
            string address = HttpContext.Request.Form["addressCtrl"].ToString();

            Order order = new Order()
            {
                Address = address,
                OrderID = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.PendingPayment,
                UserID = User.Identity.Name,                
            };

            List<OrderDetail> orderDetails = null;
            if (string.IsNullOrEmpty(idList))
            {
                orderDetails = await CreateOrderDetailsByProductAsync(
                    order.OrderID, productID, amount);
            }
            else
            {
                orderDetails = await CreateOrderDetailsByCartIDListAsync(
                    order.OrderID, idList);
            }
            foreach (OrderDetail od in orderDetails)
                order.TotalPrice += od.SubTotal;

            Guid orderID = await _orderRepository.AddAsync(order);
            // Add order failed.
            if (orderID == Guid.Empty)
                Redirect("/Home");
            foreach (OrderDetail od in orderDetails)
                await _orderDetailRepository.AddAsync(od);

            return Redirect("/Orders");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userID = User.Identity.Name;
            return View(await _orderRepository.GetOrdersAsync(userID, 50, 0));
        }
        [HttpGet]
        public async Task<IActionResult> Details([FromQuery] Guid? orderID)
        {
            if (null == orderID)
                return NotFound();
            Order order = await _orderRepository.GetAsync((Guid)orderID);
            if (null == order)
                return NotFound();
            IEnumerable<OrderDetail> orderDetails = await _orderDetailRepository.GetOrderDetailsAsync((Guid)orderID, 50, 0);
            order.OrderItems = orderDetails.ToList();
            return View(order);
        }
        private async Task<List<OrderDetail>> CreateOrderDetailsByCartIDListAsync(Guid orderID, string idList)
        {
            List<OrderDetail> items = new List<OrderDetail>();
            if (string.IsNullOrEmpty(idList))
                return items;

            string userID = HttpContext.User.Identity.Name;
            CartItem tempItem = null;
            OrderDetail DetailsItem = null;
            Guid tempGuid = Guid.Empty;
            string[] cartIDList = idList.Split(new char[] { '|' });
            foreach (string cartID in cartIDList)
            {
                if (Guid.TryParse(cartID, out tempGuid))
                {
                    tempItem = await _cartItemRepository.GetByID(tempGuid);
                    if (null == tempItem)
                        continue;
                    DetailsItem = new OrderDetail()
                    {
                        OrderID          = orderID,
                        PlaceDate        = tempItem.LastUpdatedDateTime,
                        ProductID        = tempItem.ProductID,
                        ProductName      = tempItem.ProductName,
                        ThumbImagePath   = tempItem.ThumbImagePath,
                        Quantity         = tempItem.Quantity,
                        SubTotal         = tempItem.SubTotal,
                        UnitPrice        = tempItem.UnitPrice
                    };
                    items.Add(DetailsItem);
                }
            }
            return items;
        }
        private async Task<List<OrderDetail>> CreateOrderDetailsByProductAsync(Guid orderID, string ProductID, int amount)
        {
            List<OrderDetail> items = new List<OrderDetail>();
            if (string.IsNullOrEmpty(ProductID))
                return items;

            string userID = HttpContext.User.Identity.Name;
            CartItem tempItem = new CartItem();
            Guid tempGuid = Guid.Empty;
            if (!Guid.TryParse(ProductID, out tempGuid))
                return items;

            Product product = await _productRepository.GetAsync(tempGuid);
            if (null == product)
                return items;
            OrderDetail orderDetail = new OrderDetail()
            {
                OrderID = orderID,
                PlaceDate = DateTime.Now,
                ProductName = product.ProductName,
                ProductID = product.ProductID,
                ThumbImagePath = product.ThumbnailImage,
                UnitPrice = product.UnitPrice,
                Quantity = amount,
                SubTotal = product.UnitPrice * amount
            };
            items.Add(orderDetail);
            return items;
        }
    }
}