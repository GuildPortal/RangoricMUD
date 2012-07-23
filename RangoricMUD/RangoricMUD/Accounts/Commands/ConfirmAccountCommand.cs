using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Commands;

namespace RangoricMUD.Accounts.Commands
{
    public class ConfirmAccountCommand: BaseCommand<bool>, IConfirmAccountCommand
    {
        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}