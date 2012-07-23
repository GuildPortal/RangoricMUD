using System;
using RangoricMUD.Commands;

namespace RangoricMUD.Accounts.Commands
{
    public class SendConfirmationCommand:BaseCommand<bool>, ISendConfirmationCommand
    {
        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}