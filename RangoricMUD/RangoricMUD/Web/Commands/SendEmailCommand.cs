using System;
using RangoricMUD.Commands;
using RangoricMUD.Web.Models;

namespace RangoricMUD.Web.Commands
{
    public class SendEmailCommand<TType>:BaseCommand<bool>, ISendEmailCommand<TType>
    {
        private SendEmailModel<TType> mSendEmailModel;

        public SendEmailCommand(SendEmailModel<TType> tSendEmailModel)
        {
            mSendEmailModel = tSendEmailModel;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}