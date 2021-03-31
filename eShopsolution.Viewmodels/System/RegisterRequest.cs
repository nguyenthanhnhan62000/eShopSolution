using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.System
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String ConfirmPassword { get; set; }


    }
}
