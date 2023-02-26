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
    public class RoleMappingAPIController : ApiController
    {
        private FacultyManagementSystemEntities db = new FacultyManagementSystemEntities();

        // GET: api/RoleMappingAPI
        public IQueryable<Tbl_RoleMapping> GetTbl_RoleMapping()
        {
            return db.Tbl_RoleMapping;
        }

        // GET: api/RoleMappingAPI/5
        [ResponseType(typeof(Tbl_RoleMapping))]
        public IHttpActionResult GetTbl_RoleMapping(int id)
        {
            Tbl_RoleMapping tbl_RoleMapping = db.Tbl_RoleMapping.Find(id);
            if (tbl_RoleMapping == null)
            {
                return NotFound();
            }

            return Ok(tbl_RoleMapping);
        }

        // PUT: api/RoleMappingAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_RoleMapping(int id, Tbl_RoleMapping tbl_RoleMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_RoleMapping.RoleMappingID)
            {
                return BadRequest();
            }

            db.Entry(tbl_RoleMapping).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_RoleMappingExists(id))
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

        // POST: api/RoleMappingAPI
        [ResponseType(typeof(Tbl_RoleMapping))]
        public IHttpActionResult PostTbl_RoleMapping(Tbl_RoleMapping tbl_RoleMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_RoleMapping.Add(tbl_RoleMapping);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_RoleMapping.RoleMappingID }, tbl_RoleMapping);
        }

        // DELETE: api/RoleMappingAPI/5
        [ResponseType(typeof(Tbl_RoleMapping))]
        public IHttpActionResult DeleteTbl_RoleMapping(int id)
        {
            Tbl_RoleMapping tbl_RoleMapping = db.Tbl_RoleMapping.Find(id);
            if (tbl_RoleMapping == null)
            {
                return NotFound();
            }

            db.Tbl_RoleMapping.Remove(tbl_RoleMapping);
            db.SaveChanges();

            return Ok(tbl_RoleMapping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_RoleMappingExists(int id)
        {
            return db.Tbl_RoleMapping.Count(e => e.RoleMappingID == id) > 0;
        }
    }
}