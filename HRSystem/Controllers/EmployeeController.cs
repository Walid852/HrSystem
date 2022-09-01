using HRSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HRSystem.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RetriveAllEmployees()
        {
            IEnumerable<Employee> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployees").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                return View(empList);
        }
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployee/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<Employee>().Result);
        }
            [HttpGet]
        public ActionResult CreatEmployee()
        {
            IEnumerable<Department> DeparmentList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Department/GetDepartments").Result;
            
            DeparmentList = response.Content.ReadAsAsync<IEnumerable<Department>>().Result;
            
            ViewBag.Departments = new SelectList(DeparmentList, "ID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreatEmployee(Employee employee)
        {
            IEnumerable<Employee> empList;
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployees").Result;
            empList = responsee.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            if (employee.DateOfBirth >= DateTime.Now)
            {  TempData["create"] = employee.DateOfBirth + "not logic";
                return RedirectToAction("CreatEmployee");
            }
            if (empList.Where(x=>x.Email==employee.Email).Count()!=0) 
            {
                TempData["create"] = "this email " + employee.Email + ",founded";
                return RedirectToAction("CreatEmployee");
            }
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee/PostEmployee", employee).Result;
            TempData["SuccessMessage"] = "Welcome to " + employee.Name + ",the new employee";
            return RedirectToAction("RetriveAllEmployees");
        }
        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployee/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<Employee>().Result);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployee/" + employee.EmployeeID.ToString()).Result;
            Employee emp=responsee.Content.ReadAsAsync<Employee>().Result;
            employee.Password=emp.Password;
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employee/PutEmployee/" + employee.EmployeeID, employee).Result;
            TempData["SuccessMessage"] = employee.Name + ", the data has been modified successfully";
            return RedirectToAction("RetriveAllEmployees");
        }
        public ActionResult DeleteEmployee(int id)
        {
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployee/" + id.ToString()).Result;
            Employee emp = responsee.Content.ReadAsAsync<Employee>().Result;
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employee/DeleteEmployee/" + id).Result;
            TempData["SuccessMessage"] = emp.Name + ",has been erased from employee records";
            return RedirectToAction("RetriveAllEmployees");
        }

    }
}