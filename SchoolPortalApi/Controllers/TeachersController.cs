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
    public class TeachersController : ApiController
    {
        private SchoolPortalAPIEntities1 db = new SchoolPortalAPIEntities1();

        // GET: api/Teachers
        public IQueryable<Teacher> GetTeacher()
        {
            return db.Teacher;
        }

        // GET: api/Teachers/5
        [ResponseType(typeof(ResponceTeacher))]
        public IHttpActionResult GetTeacher(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ResponceTeacher responceTeacher = new ResponceTeacher()
            {
                id = teacher.id,
                LastName = teacher.LastName,
                FirstName = teacher.FirstName,
                Birthday = teacher.Birthday,
                Email = teacher.Email,
                Gender = teacher.Gender.Name,
                MidName = teacher.MidName,
                Phone = teacher.Phone,
                Position = teacher.Position.Name,
                idUser = teacher.idUser,
                Role = teacher.User.Role.Name
            };
            return Json(responceTeacher);
        }

        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher(int id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.id)
            {
                return BadRequest();
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult PostTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teacher.Add(teacher);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teacher.id }, teacher);
        }

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult DeleteTeacher(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.Teacher.Remove(teacher);
            db.SaveChanges();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(int id)
        {
            return db.Teacher.Count(e => e.id == id) > 0;
        }
    }
}