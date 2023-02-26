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
    public class FacultyAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/FacultyAPI
        public IHttpActionResult GetTbl_Faculty()
        {
            var User = (from f in db.Tbl_Faculty
                        join d in db.Tbl_Department
                        on f.Dept_ID equals d.Dept_ID
                        join c in db.Tbl_Course
                        on f.Assign_Course_ID equals c.Course_ID
                        join u in db.tbl_UserLogin
                        on f.EmailID equals u.Email
                        select new GetFacultyData
                        {
                            FacultyID = f.Faculty_ID,
                            Faculty_Name = f.Faculty_Name,
                            Faculty_Qualification = f.Faculty_Qualification,
                            Address = f.Address,
                            ContactNo = f.ContactNo,
                            Gender = f.Gender,
                            Dept_ID = f.Dept_ID,
                            Dept_Name = d.Dept_Name,
                            Assign_Course_ID = f.Assign_Course_ID,
                            Assign_Course = c.Course_Name,
                            EmailID=u.Email,
                            Password=u.Passward
                        }).ToList();

            return Ok(User);
        }

        // GET: api/FacultyAPI/5
        [ResponseType(typeof(Tbl_Faculty))]
        public IHttpActionResult GetTbl_Faculty(int id)
        {
           // Tbl_Faculty tbl_Faculty = db.Tbl_Faculty.Find(id);
            var User = (from f in db.Tbl_Faculty
                        join d in db.Tbl_Department
                        on f.Dept_ID equals d.Dept_ID
                        join c in db.Tbl_Course
                        on f.Assign_Course_ID equals c.Course_ID
                        join u in db.tbl_UserLogin
                        on f.EmailID equals u.Email
                        where f.Faculty_ID==id
                        select new GetFacultyData
                        {
                            FacultyID = f.Faculty_ID,
                            Faculty_Name = f.Faculty_Name,
                            Faculty_Qualification = f.Faculty_Qualification,
                            Address = f.Address,
                            ContactNo = f.ContactNo,
                            Gender = f.Gender,
                            Dept_ID = f.Dept_ID,
                            Dept_Name = d.Dept_Name,
                            Assign_Course_ID = f.Assign_Course_ID,
                            Assign_Course = c.Course_Name,
                            EmailID = u.Email,
                            Password = u.Passward
                        }).ToList();
            //if (tbl_Faculty == null)
            //{
            //    return NotFound();
            //}

            return Ok(User);
        }

        // PUT: api/FacultyAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Faculty(int id, Tbl_Faculty tbl_Faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Faculty.Faculty_ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Faculty).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_FacultyExists(id))
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

        // POST: api/FacultyAPI
        [ResponseType(typeof(Tbl_Faculty))]
        public IHttpActionResult PostTbl_Faculty(Tbl_Faculty tbl_Faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Faculty.Add(tbl_Faculty);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Faculty.Faculty_ID }, tbl_Faculty);
        }

        // DELETE: api/FacultyAPI/5
        [ResponseType(typeof(Tbl_Faculty))]
        public IHttpActionResult DeleteTbl_Faculty(int id)
        {
            Tbl_Faculty tbl_Faculty = db.Tbl_Faculty.Find(id);
            if (tbl_Faculty == null)
            {
                return NotFound();
            }

            db.Tbl_Faculty.Remove(tbl_Faculty);
            db.SaveChanges();

            return Ok(tbl_Faculty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_FacultyExists(int id)
        {
            return db.Tbl_Faculty.Count(e => e.Faculty_ID == id) > 0;
        }
    }
}