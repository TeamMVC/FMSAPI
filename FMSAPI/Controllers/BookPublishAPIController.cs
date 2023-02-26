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
    public class BookPublishAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/BookPublishAPI
        public IHttpActionResult GetTbl_BookPublish()
        {
            var User = (from f in db.Tbl_Faculty
                        join b in db.Tbl_BookPublish
                        on f.Faculty_ID equals b.FacultyID
                        select new GetBookPublishData
                        {
                            PublishID = b.PublishID,
                            FacultyID = f.Faculty_ID,
                            Faculty_Name = f.Faculty_Name,
                            BookName = b.BookName,
                            Publish_Date =b.Publish_Date                           
                        }).ToList();

            return Ok(User);
        }

        // GET: api/BookPublishAPI/5
        [ResponseType(typeof(Tbl_BookPublish))]
        public IHttpActionResult GetTbl_BookPublish(int id)
        {
            Tbl_BookPublish tbl_BookPublish = db.Tbl_BookPublish.Find(id);
            if (tbl_BookPublish == null)
            {
                return NotFound();
            }

            return Ok(tbl_BookPublish);
        }

        // PUT: api/BookPublishAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_BookPublish(int id, Tbl_BookPublish tbl_BookPublish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_BookPublish.PublishID)
            {
                return BadRequest();
            }

            db.Entry(tbl_BookPublish).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_BookPublishExists(id))
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

        // POST: api/BookPublishAPI
        [ResponseType(typeof(Tbl_BookPublish))]
        public IHttpActionResult PostTbl_BookPublish(Tbl_BookPublish tbl_BookPublish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_BookPublish.Add(tbl_BookPublish);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_BookPublish.PublishID }, tbl_BookPublish);
        }

        // DELETE: api/BookPublishAPI/5
        [ResponseType(typeof(Tbl_BookPublish))]
        public IHttpActionResult DeleteTbl_BookPublish(int id)
        {
            Tbl_BookPublish tbl_BookPublish = db.Tbl_BookPublish.Find(id);
            if (tbl_BookPublish == null)
            {
                return NotFound();
            }

            db.Tbl_BookPublish.Remove(tbl_BookPublish);
            db.SaveChanges();

            return Ok(tbl_BookPublish);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_BookPublishExists(int id)
        {
            return db.Tbl_BookPublish.Count(e => e.PublishID == id) > 0;
        }
    }
}