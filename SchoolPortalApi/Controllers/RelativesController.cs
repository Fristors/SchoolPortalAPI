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
using SchoolPortalApi;
using SchoolPortalApi.Models;

namespace SchoolPortalApi.Controllers
{
    public class RelativesController : ApiController
    {
        private SchoolPortalAPIEntities1 db = new SchoolPortalAPIEntities1();

        // GET: api/Relatives
        public IQueryable<Relative> GetRelative()
        {
            return db.Relative;
        }

        // GET: api/Relatives/5
        [ResponseType(typeof(ResponceRelative))]
        public IHttpActionResult GetRelative(int id)
        {
            Relative relative = db.Relative.Find(id);
            if (relative == null)
            {
                return NotFound();
            }
            ResponceRelative responceRelative = new ResponceRelative()
            {
                id = relative.id,
                LastName = relative.LastName,
                FirstName = relative.FirstName,
                MidName = relative.MidName,
                BirthDay = relative.BirthDay,
                Gender = relative.Gender.Name,
                idUser = relative.idUser,
                Role = relative.User.Role.Name
            };
            return Ok(relative);
        }

        // PUT: api/Relatives/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRelative(int id, Relative relative)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != relative.id)
            {
                return BadRequest();
            }

            db.Entry(relative).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelativeExists(id))
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

        // POST: api/Relatives
        [ResponseType(typeof(Relative))]
        public IHttpActionResult PostRelative(Relative relative)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Relative.Add(relative);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = relative.id }, relative);
        }

        // DELETE: api/Relatives/5
        [ResponseType(typeof(Relative))]
        public IHttpActionResult DeleteRelative(int id)
        {
            Relative relative = db.Relative.Find(id);
            if (relative == null)
            {
                return NotFound();
            }

            db.Relative.Remove(relative);
            db.SaveChanges();

            return Ok(relative);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelativeExists(int id)
        {
            return db.Relative.Count(e => e.id == id) > 0;
        }
    }
}