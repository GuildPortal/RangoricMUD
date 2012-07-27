using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RangoricMUD.Web.Models
{
    public class SendEmailModel<TType>
    {
        public TType Data { get; set; }
        public string View { get; set; }

        public string ToAddress { get; set; }
    }
}