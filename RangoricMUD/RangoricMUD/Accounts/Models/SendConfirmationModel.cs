using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RangoricMUD.Accounts.Models
{
    public class SendConfirmationModel
    {
        public SendConfirmationModel(string tName, string tEmail, int tConfirmationNumber)
        {
            Name = tName;
            Email = tEmail;
            ConfirmationNumber = tConfirmationNumber;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public int ConfirmationNumber { get; private set; }
    }
}