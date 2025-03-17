using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Exceptions
{
    public class PasswordChangeFailedException:Exception
    {
        public PasswordChangeFailedException() :base("Bilinmeyen xeta") { }
        public PasswordChangeFailedException(string message) : base(message) { }
        public PasswordChangeFailedException(string message, Exception? innerException) { }
       


    }
}
