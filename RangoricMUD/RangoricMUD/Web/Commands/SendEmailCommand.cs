using System.Net.Mail;
using RangoricMUD.Commands;
using RangoricMUD.Web.Models;

namespace RangoricMUD.Web.Commands
{
    public class SendEmailCommand:BaseCommand<bool>, ISendEmailCommand
    {
        private readonly SendEmailModel mSendEmailModel;

        public SendEmailCommand(SendEmailModel tSendEmailModel)
        {
            mSendEmailModel = tSendEmailModel;
        }

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
    }
}