using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPortalApi.Models
{
    public class ResponceMessages
    {
        public int idDialog { get; set; }
        public List<MessageDialog> messages { get; set; }
    }
}