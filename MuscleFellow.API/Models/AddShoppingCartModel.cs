using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuscleFellow.API.Models
{
    public class AddShoppingCartModel
    {
        public string UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Amount { get; set; }
    }
}
