//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
        public int NewID { get; set; }
        public Nullable<int> AdminID { get; set; }
        public string AdminName { get; set; }
        public Nullable<System.DateTime> PublichedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}