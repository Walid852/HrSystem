using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRSystem.Models
{
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Postion is Required")]
        public string Postion { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        public Nullable<int> Age { get; set; }
        [Required(ErrorMessage = "Salary is Required")]
        public Nullable<int> Salary { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$",ErrorMessage = "Require that at least one digit appear anywhere in the string" +
            "and at least one lowercase letter appear anywhere in the string and at least one uppercase letter appear anywhere in the string and" +
            "at least one special character appear anywhere in the string and The password must be at least 8 characters long, but no more than 32")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Of Birth is Required")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0125][0-9]{8}$")]
        public string Phone { get; set; }
        [DisplayFormat(NullDisplayText = "Not gender specified")]
        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = " Department Name is Required")]
        public int DepartmentId { get; set; }
        public string Photo { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}