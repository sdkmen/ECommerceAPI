using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class AppUserNotFoundException : Exception
    {
        public AppUserNotFoundException() : base("App user can not be found")
        {
        }

        public AppUserNotFoundException(string? message) : base(message)
        {
        }

        public AppUserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
