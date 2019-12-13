using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
   public class ApplicationUser : IdentityUser
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ShippingAddress { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
    }
}
