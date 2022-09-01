using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSystem.Models
{
    public class Admin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string Salary { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<int> AdminId { get; set; }
    }
}