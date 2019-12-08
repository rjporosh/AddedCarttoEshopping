using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Twilio.Types;

namespace Ecommerce.DatabaseContext
{
    public class ApplicationUser  : IdentityUser
    {
       
        [Required]
        [ServiceStack.DataAnnotations.Unique]
        [EmailAddress]
        public override string Email { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public string? Address { get; set; }
        [ServiceStack.DataAnnotations.Unique]
        [Required]
        public string? Phone { get; set; }
        // public object Claims { get; internal set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

     
    }

    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
