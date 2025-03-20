using NewCommerce.Application.DTOs.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Abstractions.Configurations
{
    public interface IApplicationService
    {
       List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
