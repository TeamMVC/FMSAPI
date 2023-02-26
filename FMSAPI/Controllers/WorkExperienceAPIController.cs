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
using System.Web.Http.Cors;
using FMSAPI.Models;

namespace FMSAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WorkExperienceAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/WorkExperienceAPI
        //public IQueryable<Tbl_WorkExperience> GetTbl_WorkExperience()
        //{
        //    return db.Tbl_WorkExperience;
        //}
        public IHttpActionResult GetTbl_WorkExperience()
        {
            var User = (from f in db.Tbl_Faculty
                        join w in db.Tbl_WorkExperience
                        on f.Faculty_ID equals w.WorkID                       
                        select new GetWorkExperienceData
                        {
                            WorkID=w.WorkID,
                            FacultyID = f.Faculty_ID,
                            Faculty_Name = f.Faculty_Name,
                            Course = w.Course,
                            Skills=w.Skills,
                            Experience=w.Experience
                        }).ToList();

            return Ok(User);
        }


        // GET: api/WorkExperienceAPI/5
        [ResponseType(typeof(Tbl_WorkExperience))]
        public IHttpActionResult GetTbl_WorkExperience(int id)
        {
            Tbl_WorkExperience tbl_WorkExperience = db.Tbl_WorkExperience.Find(id);
            if (tbl_WorkExperience == null)
            {
                return NotFound();
            }

            return Ok(tbl_WorkExperience);
        }

        // PUT: api/WorkExperienceAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_WorkExperience(int id, Tbl_WorkExperience tbl_WorkExperience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_WorkExperience.WorkID)
            {
                return BadRequest();
            }

            db.Entry(tbl_WorkExperience).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_WorkExperienceExists(id))
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

        // POST: api/WorkExperienceAPI
        [ResponseType(typeof(Tbl_WorkExperience))]
        public IHttpActionResult PostTbl_WorkExperience(Tbl_WorkExperience tbl_WorkExperience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_WorkExperience.Add(tbl_WorkExperience);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_WorkExperience.WorkID }, tbl_WorkExperience);
        }

        // DELETE: api/WorkExperienceAPI/5
        [ResponseType(typeof(Tbl_WorkExperience))]
        public IHttpActionResult DeleteTbl_WorkExperience(int id)
        {
            Tbl_WorkExperience tbl_WorkExperience = db.Tbl_WorkExperience.Find(id);
            if (tbl_WorkExperience == null)
            {
                return NotFound();
            }

            db.Tbl_WorkExperience.Remove(tbl_WorkExperience);
            db.SaveChanges();

            return Ok(tbl_WorkExperience);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_WorkExperienceExists(int id)
        {
            return db.Tbl_WorkExperience.Count(e => e.WorkID == id) > 0;
        }
    }
}