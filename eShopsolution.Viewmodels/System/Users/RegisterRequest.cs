using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopsolution.Viewmodels.System
{
    public class RegisterRequest
    {
        [Display(Name ="Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        
        [Display(Name = "Hòm thư")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        public String UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = " Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }


    }
}
