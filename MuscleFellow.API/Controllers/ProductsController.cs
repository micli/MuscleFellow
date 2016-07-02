using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuscleFellow.Models;
using MuscleFellow.Data.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;
using MuscleFellow.API.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MuscleFellow.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly WebApiSettings _settings;
        public ProductsController(IProductRepository productRepository, 
            IProductImageRepository productImageRepository, IOptions<WebApiSettings> settings)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _settings = settings.Value;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string keyword, [FromQuery] int page, [FromQuery] int pagesize)
        {
            if (0 == pagesize)
                return ResponseHelper.BadRequest();

            IEnumerable<Product> products = await _productRepository.GetProductsAsync(keyword, pagesize, page);
            foreach (Product p in products)
                p.ThumbnailImage = _settings.HostName + p.ThumbnailImage;
            JsonResult result = new JsonResult(products);
            return result;
        }
        // GET api/values/5
        [HttpGet("{productID}")]
        public async Task<ActionResult> Get([FromRoute] Guid productID)
        {
            if (Guid.Empty == productID)
                return ResponseHelper.BadRequest();
            Product product = await _productRepository.GetAsync(productID);
            product.ThumbnailImage = _settings.HostName + product.ThumbnailImage;
            product.Images = await _productImageRepository.GetProductImages(product.ProductID);
            foreach (ProductImage img in product.Images)
                img.RelativeUrl = _settings.HostName + img.RelativeUrl;
            JsonResult result = new JsonResult(product);
            return result;
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product value)
        {
            if (null == value || Guid.Empty == value.ProductID
                || null == value.ProductID)
                return ResponseHelper.BadRequest();

            // 先查看当前保存的数据中是否有这么一个产品信息
            var product = await _productRepository.GetAsync(value.ProductID);
            if (null == product)
                await _productRepository.AddAsync(value);
            else
            {
                product.BrandID = value.BrandID;
                product.CategoryID = value.CategoryID;
                product.CreatedTime = value.CreatedTime;
                product.Currency = value.Currency;
                product.Description = value.Description;
                product.Height = value.Height;
                product.Images = value.Images;
                product.LastUpdateTime = value.LastUpdateTime;
                product.Length = value.Length;
                product.ProductName = value.ProductName;
                product.ThumbnailImage = value.ThumbnailImage;
                product.UnitOfLength = value.UnitOfLength;
                product.UnitOfWeight = value.UnitOfWeight;
                product.Weight = value.Width;
                product.Width = value.Width;
                await _productRepository.UpdateAsync(product);
            }

            return Ok();
        }
        // PUT api/values/5
        [HttpPut("{productID}")]
        public async Task<IActionResult> Put([FromRoute] Guid productID, [FromBody] Product value)
        {
            if (null == value || Guid.Empty == value.ProductID
                || null == value.ProductID)
                return ResponseHelper.BadRequest();

            // 先查看当前保存的数据中是否有这么一个产品信息
            var product = await _productRepository.GetAsync(value.ProductID);
            if (null == product)
                await _productRepository.AddAsync(value);
            else
            {
                product.BrandID = value.BrandID;
                product.CategoryID = value.CategoryID;
                product.CreatedTime = value.CreatedTime;
                product.Currency = value.Currency;
                product.Description = value.Description;
                product.Height = value.Height;
                product.Images = value.Images;
                product.LastUpdateTime = value.LastUpdateTime;
                product.Length = value.Length;
                product.ProductName = value.ProductName;
                product.ThumbnailImage = value.ThumbnailImage;
                product.UnitOfLength = value.UnitOfLength;
                product.UnitOfWeight = value.UnitOfWeight;
                product.Weight = value.Width;
                product.Width = value.Width;
                await _productRepository.UpdateAsync(product);
            }
            return Ok();
        }
        // DELETE api/values/5
        [HttpDelete("{productID}")]
        public async Task<IActionResult> Delete(Guid productID)
        {
            if (Guid.Empty == productID)
                return ResponseHelper.BadRequest();
            await _productRepository.DeleteAsync(productID);
            return Ok();
        }
    }
}
