using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRSystem.Models
{
    public class ArticlesResult
    {
        public Statuses Status { get; set; }

        public Error Error { get; set; }

        public int TotalResults { get; set; }

        public List<Article> Articles { get; set; }
    }
    public enum Statuses
    {
        //
        // Summary:
        //     Request was successful
        Ok,
        //
        // Summary:
        //     Request failed
        Error
    }
}