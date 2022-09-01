using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSystem.Models
{
    public partial class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MangerId { get; set; }
    }
}