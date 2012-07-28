#region License

// RangoricMUD is licensed under the Open Game License.
// The original code and assets provided in this repository are Open Game Content,
// The name RangoricMUD is product identity, and can only be used as a part of the code,
//   or in reference to this project.
// 
// More details and the full text of the license are available at:
//   https://github.com/Rangoric/RangoricMUD/wiki/Open-Game-License
// 
// RangoricMUD's home is at: https://github.com/Rangoric/RangoricMUD

#endregion

#region References

using RangoricMUD.Accounts.Data;
using RangoricMUD.Commands;
using RangoricMUD.Web.Commands;
using RangoricMUD.Web.Models;

#endregion

namespace RangoricMUD.Accounts.Commands
{
    public class SendConfirmationCommand : BaseCommand<bool>, ISendConfirmationCommand
    {
        private readonly Account mAccount;
        private readonly IWebCommandFactory mWebCommandFactory;

        public SendConfirmationCommand(IWebCommandFactory tWebCommandFactory, Account tAccount)
        {
            mWebCommandFactory = tWebCommandFactory;
            mAccount = tAccount;
        }

        #region ISendConfirmationCommand Members

        public override bool Execute()
        {
            var vBody =
                @"<p>
    Thank you for making an account on RangoricMUD " + mAccount.Name + @"!
</p>
<p>
    In order to have access to all of the features, you will have to confirm your account in game. To do this, cut and paste the number below into the confirmation number box on the site while logged in. You can get the box to appear by hitting the ""Confirm"" box in the upper right hand area. You will have to reenter your information like your account name and password.
</p>
<p>ConfirmationNumber: " +
                mAccount.ConfirmationNumber + @"</p>";
            var vModel = new SendEmailModel
                             {
                                 Body = vBody,
                                 Subject = "Please Confirm Your Account On RangoricMUD",
                                 ToAddress = mAccount.Email
                             };

            var vSendMailCommand = mWebCommandFactory.CreateSendEmailCommand(vModel);
            return vSendMailCommand.Execute();
        }

        #endregion
    }
}