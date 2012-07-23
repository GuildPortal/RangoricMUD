using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RangoricMUD.Accounts.Models
{
    public class ConfirmationEmailModel
    {
        public string SiteName { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public int ConfirmationNumber { get; set; }
    }
}