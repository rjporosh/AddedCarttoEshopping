using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Ecommerce.DatabaseContext
{
    public class ApplicationUserRole: IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

}