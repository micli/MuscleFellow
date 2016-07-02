using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuscleFellow.Models.BasicInfo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MuscleFellow.Web.Models.ShipAddress
{
    public class AddressCreationModel
    {
        public MuscleFellow.Models.ShipAddress ShipAddress { get; set; }
        public string ProvinceListName { get; set; }
        public List<SelectListItem> Provinces { get; set; }
        public string CityListName { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}
