using HRSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HRSystem.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RetriveAllRequests()
        {
            IEnumerable<Request> RequestsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Request/GetRequests").Result;
            RequestsList = response.Content.ReadAsAsync<IEnumerable<Request>>().Result;
            RequestsList = RequestsList.Where(x=>x.Type!= "Permission"&&x.status=="pending").ToList();
            return View(RequestsList);
        }
        public ActionResult RetriveAllPermission()
        {
            IEnumerable<Request> RequestsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Request/GetRequests").Result;
            RequestsList = response.Content.ReadAsAsync<IEnumerable<Request>>().Result;
            RequestsList = RequestsList.Where(x => x.Type == "Permission" && x.status == "pending");
            return View(RequestsList);
        }
        public ActionResult RetriveMyRequests(int id)
        {
            IEnumerable<Request> RequestsList;

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Request/GetRequests").Result;
            RequestsList = response.Content.ReadAsAsync<IEnumerable<Request>>().Result;
            RequestsList = RequestsList.Where(x => x.EmployeeId == id && x.Type != "Permission");
            return View(RequestsList);
        }
        public ActionResult RetriveMyPermission(int id)
        {
            IEnumerable<Request> RequestsList;

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Request/GetRequests").Result;
            RequestsList = response.Content.ReadAsAsync<IEnumerable<Request>>().Result;
            RequestsList = RequestsList.Where(x => x.EmployeeId == id && x.Type== "Permission");
            return View(RequestsList);
        }

        public ActionResult Details(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Request/GetRequest/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<Request>().Result);
        }
        [HttpGet]
        public ActionResult CreatRequest()
        {
         
            return View();
        }
        [HttpGet]
        public ActionResult CreatPermission()
        {

            return View("CreatPermission");
        }

        [HttpPost]
        public ActionResult CreatRequest( Request request)
        {
            IEnumerable<Request> RequestsList;
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Request/GetRequests").Result;
            RequestsList = responsee.Content.ReadAsAsync<IEnumerable<Request>>().Result;

            int num;
            while (true)
            {
                Random rnd = new Random();
                num = rnd.Next(1000);
                if (RequestsList.Where(x => x.ID == num).Count() == 0)
                {
                    request.ID = num;
                    break;
                }


            }
            if (request.Type != null)
            {
                request.NumberOfDay = (int?)(request.To - request.from).TotalDays;
                request.NumberOfHours = (int?)(request.To - request.from).TotalHours;
            }
            else
            {
                request.Type = "Permission";
                request.NumberOfDay = 0;
                request.from=DateTime.Today;
                request.To=DateTime.Today;
            }
            request.status = "pending";
            HttpResponseMessage responseee = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployee/" + Session["EmployeeID"].ToString()).Result;
            Employee emp = responseee.Content.ReadAsAsync<Employee>().Result;
            request.EmployeeId = emp.EmployeeID;
            request.EmployeeName = emp.Name;
            request.DepartmentId = emp.DepartmentId;
            request.HrName = " ";


            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Request/PostRequest", request).Result;
            TempData["SuccessMessage"] = "your request is being executed";
            if (request.Type != "Permission")
            {
                return RedirectToAction("RetriveMyRequests/" + request.EmployeeId);
            }
            else
            {
                return RedirectToAction("RetriveMyPermission/" + request.EmployeeId);
            }
        }
        public ActionResult UpdateRequest(int id)
        {
            IEnumerable<string> ListOfStataus = new string[] {"pending", "Accept", "Rejected"};
            ViewBag.Status = new SelectList(ListOfStataus);
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Request/GetRequest/" + id.ToString()).Result;
            Request request = response.Content.ReadAsAsync<Request>().Result;
            return View(request);
        }
        [HttpPost]
        public ActionResult UpdateRequest(Request request)
        {
            if (Session["AdminID"] != null)
            {
                HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Admin/GetAdmin/" + Session["AdminID"].ToString()).Result;
                Admin admin=responsee.Content.ReadAsAsync<Admin>().Result;
                request.HrName = admin.Name;
            }
            else if (Session["EmployeeID"] != null)
            {
                HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployee/" + Session["EmployeeID"].ToString()).Result;
                Employee employee = responsee.Content.ReadAsAsync<Employee>().Result;
                request.HrName = " ";
            }
            request.NumberOfDay = (int?)(request.To - request.from).TotalDays;
            request.NumberOfHours = (int?)(request.To - request.from).TotalHours;
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Request/PutRequest/" + request.ID, request).Result;

            TempData["SuccessMessage"] = " the data has been modified successfully";
            if (Session["AdminID"] != null)
            {
                return RedirectToAction("RetriveAllRequests");
            }
            else
            {
                return RedirectToAction("RetriveMyRequests/" + request.EmployeeId);
            }
        }
        
        public ActionResult DeleteRequest(int id)
        {
           
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Request/DeleteRequest/" + id).Result;
            TempData["SuccessMessage"] = "Request,has been erased from Requests records";
            return RedirectToAction("RetriveMyRequests/" + Session["EmployeeID"]);
        }

    }
}