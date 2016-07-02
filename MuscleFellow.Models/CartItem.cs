using System;

namespace MuscleFellow.Models
{
    public class CartItem
    {
        public Guid CartID { get; set; }
        public string UserID { get; set; }
        public string SessionID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string ThumbImagePath { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float SubTotal { get; set; }
        public DateTime LastUpdatedDateTime { get; set; } 
        public DateTime CreatedDateTime { get; set; }
    }
}
