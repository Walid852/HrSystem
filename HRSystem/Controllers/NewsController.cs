using HRSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HRSystem.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RetriveAllNews()
        {
            int x = 0;
            IEnumerable<News> newsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("News/GetNews").Result;
            newsList = response.Content.ReadAsAsync<IEnumerable<News>>().Result;
            return View(newsList);
        }
        
        [HttpGet]
        public ActionResult CreatNews()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult CreatNews(News news)
        {
            news.PublichedDate = DateTime.Now;
            
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Admin/GetAdmin/" + Session["AdminID"].ToString()).Result;
            Admin admin=responsee.Content.ReadAsAsync<Admin>().Result;
            IEnumerable<News> newsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("News/GetNews").Result;
            newsList = response.Content.ReadAsAsync<IEnumerable<News>>().Result;
            //news.AdminName = admin.Name;
            news.AdminID = admin.AdminId;
            
            int num;
            while (true)
            {
                Random rnd = new Random();
                num = rnd.Next(1000);
                if (newsList.Where(x => x.NewID == num).Count() == 0)
                {
                    news.NewID = num;
                    break;
                }


            }
            HttpResponseMessage responseee = GlobalVariables.WebApiClient.PostAsJsonAsync("News/PostNews", news).Result;
            TempData["SuccessMessage"] = "Sucess created news";
            return RedirectToAction("RetriveAllNews");
        }
        [HttpGet]
        public ActionResult UpdateNews(int id)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("News/GetNews/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<News>().Result);
        }
        [HttpPost]
        public ActionResult UpdateNews(News news)
        {

            news.PublichedDate = DateTime.Now;
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Admin/GetAdmin/" + Session["AdminID"].ToString()).Result;
            Admin admin = responsee.Content.ReadAsAsync<Admin>().Result;
            //news.AdminName = admin.Name;
            news.AdminID = admin.AdminId;
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("News/PutNews/" + news.NewID, news).Result;
            TempData["SuccessMessage"] =  "the data has been modified successfully";
            return RedirectToAction("RetriveAllNews");
        }
        public ActionResult DeleteNews(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("News/DeleteNews/" + id).Result;
            TempData["SuccessMessage"] = "erased from News records";
            return RedirectToAction("RetriveAllNews");
        }
    }
}