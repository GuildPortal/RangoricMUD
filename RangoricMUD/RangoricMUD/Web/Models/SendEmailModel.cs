using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RangoricMUD.Web.Models
{
    public class SendEmailModel
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string ToAddress { get; set; }
    }
}