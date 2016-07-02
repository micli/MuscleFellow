using Microsoft.AspNetCore.Mvc;
using MuscleFellow.Data;
using MuscleFellow.Data.Interfaces;
using MuscleFellow.Web.Models.Home;
using System.Linq;
using System.Threading.Tasks;

namespace MuscleFellow.Web.Controllers
{
    public class HomeController : Controller
    {
        #region --- Private members ---
        private readonly IBrandRepository _brandRepository;
        private readonly IProductRepository _productRepository;
        private readonly int _maxProductCount = 50;
        #endregion

        public HomeController(IBrandRepository brandRepository,
            IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.Keys.Contains("UserSession"))
            {
                HttpContext.Session.Set("UserSession",
                  System.Text.Encoding.UTF8.GetBytes("SessionCreation"));
            }
            HomePageViewModel hvm = new HomePageViewModel();
            hvm.Brands = await _brandRepository.GetAllAsync();
            hvm.Products = (await _productRepository.GetPopularProductsAsync(_maxProductCount)).ToList();
            return View(hvm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
