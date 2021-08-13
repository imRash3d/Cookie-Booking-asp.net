using CookieBooking.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Constraint
{
    public class StaticKeyValue
    {
        public static KeyValue User = new KeyValue { Key = "user", Value = "User" };
        public static KeyValue Product = new KeyValue { Key = "product", Value = "Product" };
    }
}
