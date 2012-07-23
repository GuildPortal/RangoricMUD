using System;
using RangoricMUD.Commands;

namespace RangoricMUD.Web.Commands
{
    public class SendEmailCommand:BaseCommand<bool>, ISendEmailCommand
    {
        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}