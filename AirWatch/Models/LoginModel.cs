using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirWatch.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string ErrorMessage { get; set; }
    }
}