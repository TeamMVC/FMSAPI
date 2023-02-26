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
    public class RoleAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/RoleAPI
        public IQueryable<tbl_Role> Gettbl_Role()
        {
            return db.tbl_Role;
        }

        // GET: api/RoleAPI/5
        [ResponseType(typeof(tbl_Role))]
        public IHttpActionResult Gettbl_Role(int id)
        {
            tbl_Role tbl_Role = db.tbl_Role.Find(id);
            if (tbl_Role == null)
            {
                return NotFound();
            }

            return Ok(tbl_Role);
        }

        // PUT: api/RoleAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Role(int id, tbl_Role tbl_Role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Role.RoleId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_RoleExists(id))
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

        // POST: api/RoleAPI
        [ResponseType(typeof(tbl_Role))]
        public IHttpActionResult Posttbl_Role(tbl_Role tbl_Role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Role.Add(tbl_Role);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Role.RoleId }, tbl_Role);
        }

        // DELETE: api/RoleAPI/5
        [ResponseType(typeof(tbl_Role))]
        public IHttpActionResult Deletetbl_Role(int id)
        {
            tbl_Role tbl_Role = db.tbl_Role.Find(id);
            if (tbl_Role == null)
            {
                return NotFound();
            }

            db.tbl_Role.Remove(tbl_Role);
            db.SaveChanges();

            return Ok(tbl_Role);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_RoleExists(int id)
        {
            return db.tbl_Role.Count(e => e.RoleId == id) > 0;
        }
    }
}