using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuscleFellow.Models
{
    public class ShipAddress
    {
        private readonly string Sepertator = ", ";
        public int AddressID { get; set; }
        public string UserID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 4)]
        public string Address { get; set; }
        public string ZipCode { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Receiver { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public override string ToString()
        {
            return CompositeAddress();
        }
        private string CompositeAddress()
        {
            StringBuilder sb = new StringBuilder(200);
            if (!string.IsNullOrEmpty(Province))
            {
                sb.Append(Province);
                if (!string.IsNullOrEmpty(City))
                    sb.Append(Sepertator + City);
            }
            if (!string.IsNullOrEmpty(Address))
                sb.Append(Sepertator + Address);
            if (!string.IsNullOrEmpty(Receiver))
                sb.Append(Sepertator + Receiver + "收");
            if (!string.IsNullOrEmpty(ZipCode))
                sb.Append(Sepertator + "邮编:" + ZipCode);
            if (!string.IsNullOrEmpty(PhoneNumber))
                sb.Append(Sepertator + "电话:" + PhoneNumber);
            return sb.ToString();
        }
    }
}
