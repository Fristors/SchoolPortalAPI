using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPortalApi.Models
{
    public class DialogUser
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
    }
}