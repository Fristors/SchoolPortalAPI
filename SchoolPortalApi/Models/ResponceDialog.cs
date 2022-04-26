using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPortalApi.Models
{
    public class ResponceDialog
    {
        public int Id { get; set; }
        public DialogUser SecondUser { get; set; }
    }
}