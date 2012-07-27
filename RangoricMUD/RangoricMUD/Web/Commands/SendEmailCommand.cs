using System.Net.Mail;
using RangoricMUD.Commands;
using RangoricMUD.Web.Models;

namespace RangoricMUD.Web.Commands
{
    public class SendEmailCommand<TType>:BaseCommand<bool>, ISendEmailCommand<TType>
    {
        private readonly SendEmailModel<TType> mSendEmailModel;

        public SendEmailCommand(SendEmailModel<TType> tSendEmailModel)
        {
            mSendEmailModel = tSendEmailModel;
        }

        public override bool Execute()
        {
            var vBody = !string.IsNullOrWhiteSpace(mSendEmailModel.View)
                            ? Extensions.RenderPartialToString(mSendEmailModel.View, mSendEmailModel)
                            : mSendEmailModel.Data.ToString();

            const string vSubject = "Please confirm your new account";

            var vTo = mSendEmailModel.ToAddress;
            var vMessage = new MailMessage
                               {
                                   IsBodyHtml = true,
                                   Body = vBody,
                                   Subject = vSubject
                               };
            vMessage.To.Add(vTo);
            var vClient = new SmtpClient();
            vClient.Send(vMessage);
            return true;
        }
    }
}