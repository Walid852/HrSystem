using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class EmployeeController : ApiController
    {
        private HrDbContext db = new HrDbContext();

        // GET: api/Employee
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // GET: api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Where(x=>x.EmployeeID==id).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        [ResponseType(typeof(Employee))]
        public IHttpActionResult VerifyEmployee(Login login)
        {
            Employee employee = db.Employees.Where(x => x.Username == login.Username).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                var md5 = new MD5CryptoServiceProvider();
                var data = Encoding.ASCII.GetBytes(login.Password);
                var md5data = md5.ComputeHash(data);

                string hex = BitConverter.ToString(md5data);
                login.Password = hex.Replace("-", "");
                if (employee.Password.Equals(login.Password))
                {
                    return Ok(employee);
                }
                else
                {
                    return BadRequest();
                }
               
            }
            


        }



        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {

           
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(employee.Password);
            var md5data = md5.ComputeHash(data);

            string hex = BitConverter.ToString(md5data);
            employee.Password = hex.Replace("-", "");
            db.Entry(employee).State = EntityState.Modified;

            try
            { 
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employee
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(employee.Password);
            var md5data = md5.ComputeHash(data);

            string hex = BitConverter.ToString(md5data);
            employee.Password = hex.Replace("-", "");
            /*string filename = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
            string extension=Path.GetExtension(employee.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString() + extension;
            employee.Photo = "~/API/Photo/"+filename;
            filename=Path.Combine(Path.GetDirectoryName(employee.ImageFile.FileName), filename);
            employee.ImageFile.SaveAs(filename);*/
            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Where(x=>x.EmployeeID==id).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

      /* protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}