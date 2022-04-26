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
    public class MessagesDialogController : ApiController
    {
        private SchoolPortalAPIEntities1 db = new SchoolPortalAPIEntities1();

        // GET: api/Messages
        public IQueryable<Message> GetMessage()
        {
            return db.Message;
        }

        // GET: api/MessagesDialog/5
        [ResponseType(typeof(ResponceMessages))]
        public IHttpActionResult GetMessage(int id)
        {
            Dialog dialog = db.Dialog.Find(id);
            if (dialog == null)
            {
                return NotFound();
            }
            List<MessageDialog> message = new List<MessageDialog>();
            foreach(Message m in db.Message.Where(u => u.idDialog == id).ToList())
            {
                message.Add(new MessageDialog()
                {
                    Id = m.id,
                    Message = m.Message1,
                    Date = m.Date,
                    idUser = m.idUser
                });
            }
            ResponceMessages responceMessages = new ResponceMessages()
            {
                idDialog = dialog.id,
                messages = message
            };
            return Json(responceMessages);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.id)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Message.Add(message);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = message.id }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Message.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Message.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Message.Count(e => e.id == id) > 0;
        }
    }
}