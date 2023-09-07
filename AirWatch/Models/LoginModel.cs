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
        public string MessageType { get; set; }
        public RetrievalModel retrievalData { get; set; }
        public bool confirmedRetrievalData { get; set; }
    }
    public class RetrievalModel
    {
        public string email { get; set; }
        public string activationcode { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string otp { get; set; }
    }
}