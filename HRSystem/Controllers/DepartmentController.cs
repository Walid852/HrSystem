using HRSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HRSystem.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RetriveAllDepartment()
        {
            IEnumerable<Department> DeparmentList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Department/GetDepartments").Result;
            DeparmentList = response.Content.ReadAsAsync<IEnumerable<Department>>().Result;
            return View(DeparmentList);
        }
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Department/GetDepartment/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<Department>().Result);
        }
        [HttpGet]
        public ActionResult CreateDepartment()
        {
            IEnumerable<Employee> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployees").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            ViewBag.Employees = new SelectList(empList, "EmployeeID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            IEnumerable<Department> DeparmentList;
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Department/GetDepartments").Result;
            DeparmentList = responsee.Content.ReadAsAsync<IEnumerable<Department>>().Result;
            int num;
            while (true)
            {
                Random rnd = new Random();
                num = rnd.Next(1000);
                if (DeparmentList.Where(x => x.Id == num).Count()==0)
                {
                    department.Id = num;
                    break;
                }
                

            }

            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Department/PostDepartment/", department).Result;
            TempData["SuccessMessage"] = "Welcome to " + department.Name + ",the new department";
            return RedirectToAction("RetriveAllDepartment");
        }
        [HttpGet]
        public ActionResult UpdateDepartment(int id)
        {
            
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Department/GetDepartment/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<Department>().Result);
        }
        [HttpPost]
        public ActionResult UpdateDepartment(Department department)
        {
            IEnumerable<Employee> empList;
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployees").Result;
            empList = responsee.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            if (empList.Where(x => x.EmployeeID == department.MangerId).Count() == 0)
            {
                TempData["Update"] = department.MangerId + ",not found";
                return RedirectToAction("UpdateDepartment");
            }
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Department/PutDepartment/" + department.Id, department).Result;
            TempData["SuccessMessage"] = department.Name + ", the data has been modified successfully";
            return RedirectToAction("RetriveAllDepartment");
        }
        public ActionResult DeleteDepartment(int id)
        {
            
            HttpResponseMessage responsee = GlobalVariables.WebApiClient.GetAsync("Department/GetDepartment/" + id.ToString()).Result;
            Department department = responsee.Content.ReadAsAsync<Department>().Result;
            IEnumerable<Employee> empList;
            HttpResponseMessage responseee = GlobalVariables.WebApiClient.GetAsync("Employee/GetEmployees").Result;
            empList = responseee.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            if(empList.Where(x=>x.DepartmentId==id).Count() == 0) 
            {
                TempData["Delete"] = department.Name + ",can't be Deleted";
                return RedirectToAction("RetriveAllDepartment");
            }
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Department/DeleteDepartment/" + id).Result;
            TempData["SuccessMessage"] = department.Name + ",has been erased from department records";
            return RedirectToAction("RetriveAllDepartment");
        }
    }
}