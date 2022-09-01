using HRSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HRSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Login login = new Login();
            login.Username = username;
            login.Password = password;


            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee/VerifyEmployee", login).Result;
            Employee emp= response.Content.ReadAsAsync<Employee>().Result;
           
            if (emp == null)
            {
                HttpResponseMessage responsee = GlobalVariables.WebApiClient.PostAsJsonAsync("Admin/VerifyAdmin", login).Result;
                Admin admin = responsee.Content.ReadAsAsync<Admin>().Result;
                if (admin != null)
                {
                    Session["AdminID"] = admin.AdminId.ToString();
                    TempData["LoginMessage"] = "Welcome to " +admin.Name+ ",succeed login";
                    return RedirectToAction("index", "Home");
                }
            }
            else if (emp != null)
            {
                
               Session["EmployeeID"] = emp.EmployeeID.ToString();
                TempData["LoginMessage"] = "Welcome to " + emp.Name + ",succeed login";
                return RedirectToAction("index", "Home");
            }

            TempData["LoginMessage"] = "Hmmmmm,username or password isn't correct";
            return RedirectToAction("Login","Login");
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

    }
}