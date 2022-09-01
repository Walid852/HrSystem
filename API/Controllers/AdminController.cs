using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class AdminController : ApiController
    {
        private HrDbContext db = new HrDbContext();

        // GET: api/Admin
        public IQueryable<Admin> GetAdmins()
        {
            return db.Admins;
        }

        // GET: api/Admin/5
        [ResponseType(typeof(Admin))]
        public IHttpActionResult GetAdmin(int id)
        {
            Admin admin = db.Admins.Where(x=>x.AdminId==id).FirstOrDefault();
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }
        public IHttpActionResult VerifyAdmin(Login login)
        {
            Admin admin = db.Admins.Where(x => x.Username== login.Username).FirstOrDefault();
            if (admin == null)
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

                var md52 = new MD5CryptoServiceProvider();
                string real = admin.Password.Trim();
                var data2 = Encoding.ASCII.GetBytes(real);
                var md5data2 = md52.ComputeHash(data2);
                string hex2 = BitConverter.ToString(md5data2);
                real = hex2.Replace("-", "");
                if (real.Equals(login.Password))
                {
                    
                    return Ok(admin);
                }
                else
                {
                    return BadRequest();
                }
            }


        }

        // PUT: api/Admin/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdmin(string id, Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.Username)
            {
                return BadRequest();
            }

            db.Entry(admin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admin
        [ResponseType(typeof(Admin))]
        public IHttpActionResult PostAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Admins.Add(admin);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdminExists(admin.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = admin.Username }, admin);
        }

        // DELETE: api/Admin/5
        [ResponseType(typeof(Admin))]
        public IHttpActionResult DeleteAdmin(string id)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            db.Admins.Remove(admin);
            db.SaveChanges();

            return Ok(admin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminExists(string id)
        {
            return db.Admins.Count(e => e.Username == id) > 0;
        }
    }
}