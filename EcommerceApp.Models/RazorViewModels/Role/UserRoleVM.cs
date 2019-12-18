using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Role
{
    public class UserRoleVM
    {
        public string UserId { get; set; }
        //public string RoleId { get; set; }
        public string UserName { get; set; }
        public bool isSelected { get; set; }
    }
}
