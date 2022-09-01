using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HRSystem
{
    public class NewsApiVariables
    {
        public static HttpClient WebApiClient = new HttpClient();

        static NewsApiVariables()
        {
           /* var sortby = "popularity";
            var apiKey = "f93820014ba24c1ba69d51af1009dfbd";
            var method = "everything";
            var URL = "https://newsapi.org/v2/"+ method + "?q=apple&from=" + DateTime.Now + "&to="+ DateTime.Now+"&sortBy="+sortby+"&apiKey="+apiKey;
            //https://newsapi.org/v2/everything?q=apple&from=2022-08-24&to=2022-08-24&sortBy=popularity&apiKey=f93820014ba24c1ba69d51af1009dfbd
           */
            WebApiClient.BaseAddress = new Uri("https://newsapi.org/v2/");
            WebApiClient.DefaultRequestHeaders.Clear();
            //WebApiClient.DefaultRequestHeaders.UserAgent.Add();
            
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}