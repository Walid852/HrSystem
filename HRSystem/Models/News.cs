using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSystem.Models
{
    public class News
    {
        public int NewID { get; set; }
        public Nullable<int> AdminID { get; set; }
        public string AdminName { get; set; }
        public Nullable<System.DateTime> PublichedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}