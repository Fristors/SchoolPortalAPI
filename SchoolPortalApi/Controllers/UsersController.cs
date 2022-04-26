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
    public class UsersController : ApiController
    {
        private SchoolPortalAPIEntities1 db = new SchoolPortalAPIEntities1();

        // GET: api/Users
        public IQueryable<User> GetUser()
        {
            return db.User;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/login/password
        [AcceptVerbs("GET", "POST")]
        [Route("api/users/{login}/{password}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult Authorization(string login, string password)
        {
            User user = db.User.Where(x => x.Login == login && x.Password == password).Single();
            if (user == null)
            {
                return NotFound();
            }
            switch (user.Role.Name)
            {
                case "Teacher":
                    Teacher teacher = db.Teacher.Where(u => u.User.id == user.id).Single();
                    return RedirectToRoute("DefaultApi", new { controller = "Teachers", id = teacher.id });
                case "Student":
                    Student student = db.Student.Where(u => u.User.id == user.id).Single();
                    return RedirectToRoute("DefaultApi", new { controller = "Students", id = student.id });
                    
                case "Relative":
                    Relative relative = db.Relative.Where(u => u.User.id == user.id).Single();
                    return RedirectToRoute("DefaultApi", new { controller = "Relatives", id = relative.id });
                default:
                    return NotFound();
            }
        }


        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.id == id) > 0;
        }
    }
}