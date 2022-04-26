using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPortalApi.Models
{
    public class ResponceStudent
    {
        public int id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public System.DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public int idUser { get; set; }
        public string Role { get; set; }
    }
}