using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FMSAPI.Models;

namespace FMSAPI.Controllers
{
    public class UserLoginAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/UserLoginAPI
        public IQueryable<tbl_UserLogin> Gettbl_UserLogin()
        {
            return db.tbl_UserLogin;
        }

        // GET: api/UserLoginAPI/5
        [ResponseType(typeof(tbl_UserLogin))]
        public IHttpActionResult Gettbl_UserLogin(int id)
        {
            tbl_UserLogin tbl_UserLogin = db.tbl_UserLogin.Find(id);
            if (tbl_UserLogin == null)
            {
                return NotFound();
            }

            return Ok(tbl_UserLogin);
        }

        // PUT: api/UserLoginAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_UserLogin(int id, tbl_UserLogin tbl_UserLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_UserLogin.Id)
            {
                return BadRequest();
            }

            db.Entry(tbl_UserLogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_UserLoginExists(id))
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

        // POST: api/UserLoginAPI
        [ResponseType(typeof(tbl_UserLogin))]
        public IHttpActionResult Posttbl_UserLogin(tbl_UserLogin tbl_UserLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_UserLogin.Add(tbl_UserLogin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_UserLogin.Id }, tbl_UserLogin);
        }

        // DELETE: api/UserLoginAPI/5
        [ResponseType(typeof(tbl_UserLogin))]
        public IHttpActionResult Deletetbl_UserLogin(int id)
        {
            tbl_UserLogin tbl_UserLogin = db.tbl_UserLogin.Find(id);
            if (tbl_UserLogin == null)
            {
                return NotFound();
            }

            db.tbl_UserLogin.Remove(tbl_UserLogin);
            db.SaveChanges();

            return Ok(tbl_UserLogin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_UserLoginExists(int id)
        {
            return db.tbl_UserLogin.Count(e => e.Id == id) > 0;
        }
    }
}