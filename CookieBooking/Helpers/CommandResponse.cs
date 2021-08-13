using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Helpers
{
    public  class CommandResponse
    {
    //    public abstract CommandResponse();

        public bool Success { get; set; }
        public dynamic Result { get; set; }
       
        public string ErrorMessage { get; set; }
    }
}
