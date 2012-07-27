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
            var vModel = new SendEmailModel<string> {View = "", ToAddress = "test@email.com", Data = "test"};
            var vCommand = new SendEmailCommand<string>(vModel);
            vCommand.Execute();
        }
    }
}
