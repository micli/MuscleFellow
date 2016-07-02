using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuscleFellow.Models
{
    public enum OrderStatus : int
    {
        PendingPayment,
        PendingShipment,
        PendingEvaluated,
        Closed,
        Cancelled
    }
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }
        public string UserID { get; set; }
        public List<OrderDetail> OrderItems { get; set; }
        [MaxLength(128)]
        public string Address { get; set; }
        public float TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
