using MuscleFellow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuscleFellow.Web.Models.Home
{
    public class HomePageViewModel
    {
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
