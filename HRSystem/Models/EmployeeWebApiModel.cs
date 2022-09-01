using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSystem.Models
{
    public class EmployeeWebApiModel
    {
        public int Id { get; set; }

        // other properties
        public int EmployeeID { get; set; }
        
        public string Name { get; set; }
        public string Username { get; set; }
        public string Postion { get; set; }
        public Nullable<int> Age { get; set; }
       
        public Nullable<int> Salary { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
       
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        
        public string City { get; set; }
      
        public string Phone { get; set; }
        
        public string Gender { get; set; }
        
        public int DepartmentId { get; set; }
        public string Photo { get; set; }
        public string Picture { get; set; }

        // byte array to hold HttpPostedFileBase content in Web API context
        public byte[] PicData { get; set; }
    }
}