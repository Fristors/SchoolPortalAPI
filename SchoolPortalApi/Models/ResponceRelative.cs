using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPortalApi.Models
{
    public class ResponceRelative
    {
        public int id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public System.DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public int idUser { get; set; }
        public string Role { get; set; }
    }
}