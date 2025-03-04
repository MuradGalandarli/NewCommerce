using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Domain.Identity
{
    public class AppUser:IdentityUser<string>
    {
        public string? NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
    }
}
