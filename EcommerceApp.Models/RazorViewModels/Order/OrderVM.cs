using System;
using System.Text;
using Xunit;
using Xunit.Sdk;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ecommerce.Models.RazorViewModels.Order
{
    public class OrderVM
    {
        public OrderVM()
        {

        }
        public long Id { get; set; }
        //public long CustomerId { get; set; }
        [Required(ErrorMessage = "Please provide Shipment Address!")]
        public string ShippingAddress { get; set; }
        [Required(ErrorMessage = "Please Select Payment Method!")]
        public string PaymentMethod { get; set; }
        [ServiceStack.DataAnnotations.Unique]
        [Required(ErrorMessage = "Please provide OrderNo!")]
        public string OrderNo { get; set; }
        public string Phone { get; set; }
        public string? AspNetUserId { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        // public Decimal? DiscountPercentage { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<Models.ProductOrder> Products { get; set; }
        public List<Models.Order>  OrderList { get; set; }
        public List<Item> ProductList { get; set; }
    }
}
