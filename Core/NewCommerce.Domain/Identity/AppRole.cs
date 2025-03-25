using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using NewCommerce.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Domain.Identity
{
    public class AppRole:IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
       
    }
}
