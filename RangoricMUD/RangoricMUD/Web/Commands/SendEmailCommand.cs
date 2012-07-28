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

using System.Net.Mail;
using RangoricMUD.Commands;
using RangoricMUD.Web.Models;

#endregion

namespace RangoricMUD.Web.Commands
{
    public class SendEmailCommand : BaseCommand<bool>, ISendEmailCommand
    {
        private readonly SendEmailModel mSendEmailModel;

        public SendEmailCommand(SendEmailModel tSendEmailModel)
        {
            mSendEmailModel = tSendEmailModel;
        }

        #region ISendEmailCommand Members

        public override bool Execute()
        {
            var vTo = mSendEmailModel.ToAddress;
            var vMessage = new MailMessage
                               {
                                   IsBodyHtml = true,
                                   Body = mSendEmailModel.Body,
                                   Subject = mSendEmailModel.Subject
                               };
            vMessage.To.Add(vTo);
            var vClient = new SmtpClient();
            vClient.Send(vMessage);
            return true;
        }

        #endregion
    }
}