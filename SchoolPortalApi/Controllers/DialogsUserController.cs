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
    public class DialogsUserController : ApiController
    {
        private SchoolPortalAPIEntities1 db = new SchoolPortalAPIEntities1();

        // GET: api/Dialogs
        public IQueryable<Dialog> GetDialog()
        {
            return db.Dialog;
        }

        [AcceptVerbs("GET", "POST")]
        [Route("api/finddialog/idUser1={idUser1};idUser2={idUser2}")]
        [ResponseType(typeof(DialogModel))]
        public IHttpActionResult FindDialog(int idUser1, int idUser2)
        {
            Dialog dialog = db.Dialog.Where(u => u.idFirstUser == idUser1 && u.idSecondUser == idUser2).SingleOrDefault();
            if (dialog == null)
            {
                dialog = db.Dialog.Where(u => u.idFirstUser == idUser2 && u.idSecondUser == idUser1).SingleOrDefault();
                if(dialog == null)
                {
                    return NotFound();
                }
                DialogModel model1 = new DialogModel()
                {
                    id = dialog.id,
                    idFirstUser = idUser2,
                    idSecondUser = idUser1
                };
                return Json(model1);
            }
            DialogModel model = new DialogModel()
            {
                id = dialog.id,
                idFirstUser = idUser1,
                idSecondUser = idUser2
            };
            return Json(model);

        }
        // GET: api/DialogsUser/5
        [ResponseType(typeof(ResponceDialog))]
        public IHttpActionResult GetDialog(int id)
        {
            List<Dialog> dialogs = db.Dialog.Where(u => u.idFirstUser == id || u.idSecondUser == id).ToList();
            if (dialogs == null)
            {
                return NotFound();
            }
            
            List<ResponceDialog> result = new List<ResponceDialog>();
            foreach (Dialog dialog in dialogs)
            {
                switch (dialog.User1.Role.Name)
                {
                    case "Teacher":
                        Teacher teacher = db.Teacher.Where(u => u.User.id == dialog.idSecondUser).Single();
                        ResponceDialog responceDialog = new ResponceDialog()
                        {
                            Id = dialog.id,
                            SecondUser = new DialogUser()
                            {
                                Id = id,
                                FirstName = teacher.FirstName,
                                LastName = teacher.LastName,
                                MidName = teacher.MidName
                            }
                        };
                        result.Add(responceDialog);
                        break;
                    case "Student":
                        Student student = db.Student.Where(u => u.User.id == dialog.idSecondUser).Single();
                        ResponceDialog responceDialog1 = new ResponceDialog()
                        {
                            Id = dialog.id,
                            SecondUser = new DialogUser()
                            {
                                Id = id,
                                FirstName = student.FirstName,
                                LastName = student.LastName,
                                MidName = student.MidName
                            }
                        };
                        result.Add(responceDialog1);
                        break;

                    case "Relative":
                        Relative relative = db.Relative.Where(u => u.User.id == dialog.idSecondUser).Single();
                        ResponceDialog responceDialog2 = new ResponceDialog()
                        {
                            Id = dialog.id,
                            SecondUser = new DialogUser()
                            {
                                Id = id,
                                FirstName = relative.FirstName,
                                LastName = relative.LastName,
                                MidName = relative.MidName
                            }
                        };
                        result.Add(responceDialog2);
                        break;
                    default:
                        return NotFound();
                }
            }
            return Json(result);
        }

        // PUT: api/Dialogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDialog(int id, Dialog dialog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dialog.id)
            {
                return BadRequest();
            }

            db.Entry(dialog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DialogExists(id))
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

        // POST: api/Dialogs
        [ResponseType(typeof(Dialog))]
        public IHttpActionResult PostDialog(Dialog dialog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dialog.Add(dialog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dialog.id }, dialog);
        }

        // DELETE: api/Dialogs/5
        [ResponseType(typeof(Dialog))]
        public IHttpActionResult DeleteDialog(int id)
        {
            Dialog dialog = db.Dialog.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }

            db.Dialog.Remove(dialog);
            db.SaveChanges();

            return Ok(dialog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DialogExists(int id)
        {
            return db.Dialog.Count(e => e.id == id) > 0;
        }
    }
}