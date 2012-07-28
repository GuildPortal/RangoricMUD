using NUnit.Framework;
using RangoricMUD.Web.Commands;
using RangoricMUD.Web.Models;

namespace RangoricMUD.Tests.Web.Commands
{
    [TestFixture]
    public class SendMailCommandTests
    {
        [Test]
        public void LeavesEmailInDropFolder()
        {
            var vModel = new SendEmailModel {Body = "", ToAddress = "test@email.com", Subject = "test"};
            var vCommand = new SendEmailCommand(vModel);
            vCommand.Execute();
        }
    }
}
