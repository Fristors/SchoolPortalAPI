using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPortalApi.Models
{
    public class MessageDialog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public System.DateTime Date { get; set; }
        public int idUser { get; set; }
    }
}