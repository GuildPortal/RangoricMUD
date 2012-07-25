using System;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Commands;
using RangoricMUD.Web.Commands;
using RangoricMUD.Web.Models;

namespace RangoricMUD.Accounts.Commands
{
    public class SendConfirmationCommand:BaseCommand<bool>, ISendConfirmationCommand
    {
        private readonly IWebCommandFactory mWebCommandFactory;
        private readonly SendConfirmationModel mSendConfirmationModel;

        public SendConfirmationCommand(IWebCommandFactory tWebCommandFactory, SendConfirmationModel tSendConfirmationModel)
        {
            mWebCommandFactory = tWebCommandFactory;
            mSendConfirmationModel = tSendConfirmationModel;
        }

        public override bool Execute()
        {
            var vModel = new SendEmailModel<SendConfirmationModel>
                             {
                                 Data = mSendConfirmationModel,
                                 View = "",
                             };

            var vSendMailCommand = mWebCommandFactory.CreateSendEmailCommand(vModel);
            return vSendMailCommand.Execute();
        }
    }
}