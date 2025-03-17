using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Helpers
{
    public static class CustumEncoders
    {
        public static string UrlDecode(this string value)
        {
            byte[] bytes = WebEncoders.Base64UrlDecode(value);
            value = Encoding.UTF8.GetString(bytes);
            return value;
        }
        public static string UrlEncode(this string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            value = WebEncoders.Base64UrlEncode(bytes);
            return value;
        }
    }
}
