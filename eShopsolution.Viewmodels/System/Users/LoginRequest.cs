using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopsolution.Viewmodels.System
{
    public class LoginRequest
    {

        public String UserName { get; set; }
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
