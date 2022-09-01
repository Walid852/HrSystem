
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using HRSystem.Models;

/*EverythingRequest everythingRequest = new EverythingRequest
            {
                Q = "Apple",
                SortBy = SortBys.PublishedAt,
                Language = Languages.EN,
                From = DateTime.Now,
                To = DateTime.Now,
            };
            IEnumerable<Article> ArticlesList;
            string output = JsonConvert.SerializeObject(everythingRequest);
            HttpResponseMessage response = NewsApiVariables.WebApiClient.GetAsync("EverythingRequest").Result;

            ArticlesList = response.Content.ReadAsAsync<IEnumerable<Article>>().Result;
            return View(ArticlesList);
            */
/*NewsApiClient newsApiClient = new NewsApiClient("f93820014ba24c1ba69d51af1009dfbd");
IEnumerable<Article> ArticlesList;
EverythingRequest everythingRequest= new EverythingRequest
{
    Q = "Apple",
    SortBy = SortBys.PublishedAt,
    Language = Languages.EN,
    From = DateTime.Now,
    To = DateTime.Now,
};


ArticlesList = (IEnumerable<Article>)newsApiClient.GetEverything(everythingRequest);
return View(ArticlesList);*/
/*NewsApiClient newsApiClient = new NewsApiClient("f93820014ba24c1ba69d51af1009dfbd");
EverythingRequest everythingRequest = new EverythingRequest
{
    Q = "Apple",
    SortBy = SortBys.PublishedAt,
    Language = Languages.EN,
    From = DateTime.Now,
    To = DateTime.Now,
};
var 

/* string url = "";
 var StringData=new WebClient().DownloadString(url);
 dynamic JsonObject=JObject.Parse(StringData);
 List<Article> articles=new List<Article>();
 foreach (var item in JsonObject.)
 {
     articles.Add(item);
 }
 return View(articles);*/
/*
 
            //IEnumerable<Article> ArticlesList;
            
            HttpResponseMessage response = NewsApiVariables.WebApiClient.GetAsync("everything?q=apple&from=2022-08-24&to=2022-08-24&sortBy=popularity&apiKey=f93820014ba24c1ba69d51af1009dfbd").Result;
            
            //Articles articles = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Articles>(response);
            Articles ArticlesList = response.Content.ReadAsAsync<Articles>().Result;
            return View(ArticlesList);
*/
namespace HRSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public ActionResult News()
        {
            string url = "everything?q=*&from="+ DateTime.Now.ToString("M/d/yyyy")+ "&to="+ DateTime.Now.ToString("M/d/yyyy")+ "&sortBy=popularity&apiKey=f93820014ba24c1ba69d51af1009dfbd";
            HttpResponseMessage response = NewsApiVariables.WebApiClient.GetAsync(url).Result;

            //Articles articles = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Articles>(response);
            //Articles ArticlesList = response.Content.ReadAsAsync<Articles>().Result;
            ArticlesResult AR = response.Content.ReadAsAsync<ArticlesResult>().Result;
            
            return View(AR.Articles);
        }
        public ActionResult NewsEgypt()
        {
            string url = "top-headlines?country=eg&category=business&apiKey=f93820014ba24c1ba69d51af1009dfbd";
            HttpResponseMessage response = NewsApiVariables.WebApiClient.GetAsync(url).Result;
            
            //Articles articles = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Articles>(response);
            //Articles ArticlesList = response.Content.ReadAsAsync<Articles>().Result;
            ArticlesResult AR = response.Content.ReadAsAsync<ArticlesResult>().Result;

            return View("News",AR.Articles);
        }
        public ActionResult NewsUS()
        {
            string url = "top-headlines?country=us&category=business&apiKey=f93820014ba24c1ba69d51af1009dfbd";
            HttpResponseMessage response = NewsApiVariables.WebApiClient.GetAsync(url).Result;

            //Articles articles = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Articles>(response);
            //Articles ArticlesList = response.Content.ReadAsAsync<Articles>().Result;
            ArticlesResult AR = response.Content.ReadAsAsync<ArticlesResult>().Result;

            return View("News",AR.Articles);
        }
    }
}