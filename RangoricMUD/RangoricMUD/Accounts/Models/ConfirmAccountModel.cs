using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RangoricMUD.Accounts.Models
{
    public class ConfirmAccountModel
    {
        public string Name { get; set; }

        public int ConfirmationNumber { get; set; }
    }
}