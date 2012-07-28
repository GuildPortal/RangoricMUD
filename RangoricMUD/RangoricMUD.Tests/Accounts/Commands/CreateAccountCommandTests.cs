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

using System.Linq;
using Moq;
using NUnit.Framework;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Dice;
using RangoricMUD.Security;
using RangoricMUD.Tests.Utilities;

#endregion

namespace RangoricMUD.Tests.Accounts.Commands
{
    [TestFixture]
    public class CreateAccountCommandTests : BaseTests
    {
        [TestCase("Admin", "ABCDEFGHIJ", "test@email.com")]
        public void NewAccountIsAdminWhenMatchesConfigForAdmin(string tName, string tPassword, string tEmail)
        {
            var vHashProvider = new Mock<IHashProvider>();
            var vDocumentStore = GetEmbeddedDatabase;
            var vAccountCommandFactory = new Mock<IAccountCommandFactory>();
            var vSendConfirmationCommand = new Mock<ISendConfirmationCommand>();
            vAccountCommandFactory
                .Setup(t => t.CreateSendConfirmationCommand(It.IsAny<Account>()))
                .Returns(vSendConfirmationCommand.Object);


            var vCreateAccount = new CreateAccount
                                     {
                                         Name = tName,
                                         Password = tPassword,
                                         Email = tEmail
                                     };
            var vRandomProvider = new Mock<IRandomProvider>();

            var vCreateAccountCommand = new CreateAccountCommand(vDocumentStore, vHashProvider.Object,
                                                                 vAccountCommandFactory.Object, vRandomProvider.Object,
                                                                 vCreateAccount);
            vCreateAccountCommand.Execute();

            using (var vSession = vDocumentStore.OpenSession())
            {
                var vAccount = vSession.Load<Account>(tName);

                Assert.IsTrue(vAccount.Roles.Any(t => t == eRoles.Admin));
            }
        }

        [TestCase("ABC", "ABCDEFGHIJ", "test@email.com")]
        public void ExecuteReturnsDuplicateWhenDuplicateName(string tName, string tPassword, string tEmail)
        {
            var vHashProvider = new Mock<IHashProvider>();
            var vDocumentStore = GetEmbeddedDatabase;
            var vAccountCommandFactory = new Mock<IAccountCommandFactory>();
            var vSendConfirmationCommand = new Mock<ISendConfirmationCommand>();
            vAccountCommandFactory
                .Setup(t => t.CreateSendConfirmationCommand(It.IsAny<Account>()))
                .Returns(vSendConfirmationCommand.Object);


            var vCreateAccount = new CreateAccount
                                     {
                                         Name = tName,
                                         Password = tPassword,
                                         Email = tEmail
                                     };
            var vRandomProvider = new Mock<IRandomProvider>();

            var vCreateAccountCommand = new CreateAccountCommand(vDocumentStore, vHashProvider.Object,
                                                                 vAccountCommandFactory.Object, vRandomProvider.Object,
                                                                 vCreateAccount);

            vCreateAccountCommand.Execute();
            var vResult = vCreateAccountCommand.Execute();
            Assert.AreEqual(eAccountCreationStatus.DuplicateName, vResult);
        }

        [TestCase("ABC", "ABCDEFGHIJ", "test@email.com")]
        public void ExecuteReturnsSuccessWhenGood(string tName, string tPassword, string tEmail)
        {
            var vHashProvider = new Mock<IHashProvider>();
            var vDocumentStore = GetEmbeddedDatabase;
            var vAccountCommandFactory = new Mock<IAccountCommandFactory>();
            var vSendConfirmationCommand = new Mock<ISendConfirmationCommand>();
            vAccountCommandFactory
                .Setup(t => t.CreateSendConfirmationCommand(It.IsAny<Account>()))
                .Returns(vSendConfirmationCommand.Object);
            vSendConfirmationCommand.Setup(t => t.Execute()).Returns(true);


            var vCreateAccount = new CreateAccount
                                     {
                                         Name = tName,
                                         Password = tPassword,
                                         Email = tEmail
                                     };
            var vRandomProvider = new Mock<IRandomProvider>();

            var vCreateAccountCommand = new CreateAccountCommand(vDocumentStore, vHashProvider.Object,
                                                                 vAccountCommandFactory.Object, vRandomProvider.Object,
                                                                 vCreateAccount);

            var vResult = vCreateAccountCommand.Execute();
            Assert.AreEqual(eAccountCreationStatus.Success, vResult);
        }

        [TestCase("ABC", "ABCDEFGHIJ", "test@email.com")]
        public void ExecuteUsesHashProviderForHashWithSalt(string tName, string tPassword, string tEmail)
        {
            var vHashProvider = new Mock<IHashProvider>();
            var vDocumentStore = GetEmbeddedDatabase;
            var vAccountCommandFactory = new Mock<IAccountCommandFactory>();
            var vSendConfirmationCommand = new Mock<ISendConfirmationCommand>();
            vAccountCommandFactory
                .Setup(t => t.CreateSendConfirmationCommand(It.IsAny<Account>()))
                .Returns(vSendConfirmationCommand.Object);


            var vCreateAccount = new CreateAccount
                                     {
                                         Name = tName,
                                         Password = tPassword,
                                         Email = tEmail
                                     };
            var vRandomProvider = new Mock<IRandomProvider>();

            var vCreateAccountCommand = new CreateAccountCommand(vDocumentStore, vHashProvider.Object,
                                                                 vAccountCommandFactory.Object, vRandomProvider.Object,
                                                                 vCreateAccount);

            vCreateAccountCommand.Execute();
            vHashProvider.Verify(t => t.Hash(tPassword));
        }

        [TestCase("ABC", "1234567890", "test@email.com")]
        public void UsesSendEmailCommand(string tName, string tPassword, string tEmail)
        {
            var vHashProvider = new Mock<IHashProvider>();
            var vDocumentStore = GetEmbeddedDatabase;
            var vRandomProvider = new Mock<IRandomProvider>();
            var vAccountCommandFactory = new Mock<IAccountCommandFactory>();
            var vSendConfirmationCommand = new Mock<ISendConfirmationCommand>();
            vAccountCommandFactory
                .Setup(t => t.CreateSendConfirmationCommand(It.IsAny<Account>()))
                .Returns(vSendConfirmationCommand.Object);


            var vCreateAccount = new CreateAccount
                                     {
                                         Name = tName,
                                         Password = tPassword,
                                         Email = tEmail
                                     };

            var vCreateAccountCommand = new CreateAccountCommand(vDocumentStore, vHashProvider.Object,
                                                                 vAccountCommandFactory.Object, vRandomProvider.Object,
                                                                 vCreateAccount);

            vCreateAccountCommand.Execute();

            vSendConfirmationCommand.Verify(t => t.Execute());
        }

        [TestCase("ABC", "1234567890", "test@email.com")]
        public void UsesRandomProvider(string tName, string tPassword, string tEmail)
        {
            var vHashProvider = new Mock<IHashProvider>();
            var vDocumentStore = GetEmbeddedDatabase;
            var vRandomProvider = new Mock<IRandomProvider>();
            var vAccountCommandFactory = new Mock<IAccountCommandFactory>();
            var vSendConfirmationCommand = new Mock<ISendConfirmationCommand>();
            vAccountCommandFactory
                .Setup(t => t.CreateSendConfirmationCommand(It.IsAny<Account>()))
                .Returns(vSendConfirmationCommand.Object);

            var vCreateAccount = new CreateAccount
                                     {
                                         Name = tName,
                                         Password = tPassword,
                                         Email = tEmail
                                     };

            var vCreateAccountCommand = new CreateAccountCommand(vDocumentStore, vHashProvider.Object,
                                                                 vAccountCommandFactory.Object, vRandomProvider.Object,
                                                                 vCreateAccount);

            vCreateAccountCommand.Execute();

            vRandomProvider.Verify(t => t.GetInteger(100000, 2000000000));
        }
    }
}