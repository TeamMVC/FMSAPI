using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FMSAPI.Models;

namespace FMSAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/DepartmentAPI
        public IQueryable<Tbl_Department> GetTbl_Department()
        {
            return db.Tbl_Department;
        }

        // GET: api/DepartmentAPI/5
        [ResponseType(typeof(Tbl_Department))]
        public IHttpActionResult GetTbl_Department(int id)
        {
            Tbl_Department tbl_Department = db.Tbl_Department.Find(id);
            if (tbl_Department == null)
            {
                return NotFound();
            }

            return Ok(tbl_Department);
        }

        // PUT: api/DepartmentAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Department(int id, Tbl_Department tbl_Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Department.Dept_ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_DepartmentExists(id))
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

        // POST: api/DepartmentAPI
        [ResponseType(typeof(Tbl_Department))]
        public IHttpActionResult PostTbl_Department(Tbl_Department tbl_Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Department.Add(tbl_Department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Department.Dept_ID }, tbl_Department);
        }

        // DELETE: api/DepartmentAPI/5
        [ResponseType(typeof(Tbl_Department))]
        public IHttpActionResult DeleteTbl_Department(int id)
        {
            Tbl_Department tbl_Department = db.Tbl_Department.Find(id);
            if (tbl_Department == null)
            {
                return NotFound();
            }

            db.Tbl_Department.Remove(tbl_Department);
            db.SaveChanges();

            return Ok(tbl_Department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_DepartmentExists(int id)
        {
            return db.Tbl_Department.Count(e => e.Dept_ID == id) > 0;
        }
    }
}