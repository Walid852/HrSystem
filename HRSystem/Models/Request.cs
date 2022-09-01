using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRSystem.Models
{
    public class Request
    {
        public int ID { get; set; }
        public string HrName { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "From is Required")]
        public System.DateTime from { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "To is Required")]
        public System.DateTime To { get; set; }
        public string Type { get; set; }
        public Nullable<int> NumberOfDay { get; set; }
        public Nullable<int> NumberOfHours { get; set; }
        public string status { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
    }
}