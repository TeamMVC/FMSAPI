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
    public class CourseAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/CourseAPI
        public IQueryable<Tbl_Course> GetTbl_Course()
        {
            return db.Tbl_Course;
        }

        // GET: api/CourseAPI/5
        [ResponseType(typeof(Tbl_Course))]
        public IHttpActionResult GetTbl_Course(int id)
        {
            Tbl_Course tbl_Course = db.Tbl_Course.Find(id);
            if (tbl_Course == null)
            {
                return NotFound();
            }

            return Ok(tbl_Course);
        }

        // PUT: api/CourseAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Course(int id, Tbl_Course tbl_Course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Course.Course_ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_CourseExists(id))
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

        // POST: api/CourseAPI
        [ResponseType(typeof(Tbl_Course))]
        public IHttpActionResult PostTbl_Course(Tbl_Course tbl_Course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Course.Add(tbl_Course);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Course.Course_ID }, tbl_Course);
        }

        // DELETE: api/CourseAPI/5
        [ResponseType(typeof(Tbl_Course))]
        public IHttpActionResult DeleteTbl_Course(int id)
        {
            Tbl_Course tbl_Course = db.Tbl_Course.Find(id);
            if (tbl_Course == null)
            {
                return NotFound();
            }

            db.Tbl_Course.Remove(tbl_Course);
            db.SaveChanges();

            return Ok(tbl_Course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_CourseExists(int id)
        {
            return db.Tbl_Course.Count(e => e.Course_ID == id) > 0;
        }
    }
}