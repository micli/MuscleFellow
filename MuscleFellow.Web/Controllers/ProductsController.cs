using Microsoft.AspNetCore.Mvc;
using MuscleFellow.Data.Interfaces;
using MuscleFellow.Models;
using System;
using System.Threading.Tasks;

namespace MuscleFellow.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductImageRepository _prodImgRepo;
        private readonly IProductRepository _productRepo;

        public ProductsController(IProductRepository productRepo, IProductImageRepository prodImgRepo)
        {
            _productRepo = productRepo;
            _prodImgRepo = prodImgRepo;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productRepo.GetAllAsync());
        }

        // GET: Products/Details/5
        [Route(template: "Product/{id}", Name = "ProductDetails")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _productRepo.GetAsync((Guid)id);
            if (product == null)
            {
                return NotFound();
            }
            product.Images = await _prodImgRepo.GetProductImages(product.ProductID);
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepo.AddAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _productRepo.GetAsync((Guid)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepo.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _productRepo.GetAsync((Guid)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}