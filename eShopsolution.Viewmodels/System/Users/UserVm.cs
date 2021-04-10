using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopsolution.Viewmodels.System
{
    public class UserVm
    {

        public Guid Id { get; set; }

        [Display(Name ="Tên")]
        public String FirstName { get; set; }

        [Display(Name = "Họ")]
        public String LastName { get; set; }

        [Display(Name = "Số điện thoại")]
        public String PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        public String UserName { get; set; }

        [Display(Name = "Email")]
        public String Email { get; set; }

        [Display(Name = "Ngày Sinh")]
        public DateTime Dob { get; set; }

        public IList<String> Roles { get; set; }

    }
}
