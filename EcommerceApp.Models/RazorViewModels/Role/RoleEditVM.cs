using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Role
{
    public class RoleEditVM
    {
        public RoleEditVM()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
