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

using Moq;
using NUnit.Framework;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Dice;
using RangoricMUD.Security;
using RangoricMUD.Tests.Utilities;
using Raven.Client;

#endregion

namespace RangoricMUD.Tests.Accounts.Commands
{
    [TestFixture]
    public class LoginAccountCommandTests : BaseTests
    {
        private class TestObjects
        {
            public IDocumentStore DocumentStore;
            public LoginAccountCommand GoodLoginAccountCommand;
            public Mock<IHashProvider> HashProvider;
        }

        private const string cName = "Name";
        private const string cEmail = "Email";
        private const string cPassword = "Password";
        private const string cHash = "Hash";

        private TestObjects Setup()
        {
            var vObjects = new TestObjects();
            vObjects.HashProvider = new Mock<IHashProvider>();
            vObjects.HashProvider.Setup(t => t.Hash(cPassword)).Returns(cHash);
            vObjects.HashProvider.Setup(t => t.CheckHash(cPassword, cHash)).Returns(true);

            vObjects.DocumentStore = GetEmbeddedDatabase;

            var vNewAccount = new CreateAccount
                                  {
                                      Name = cName,
                                      Email = cEmail,
                                      Password = cPassword
                                  };
            var vAccountCommandFactory = new Mock<IAccountCommandFactory>();
            var vSendConfirmationCommand = new Mock<ISendConfirmationCommand>();
            vAccountCommandFactory
                .Setup(t => t.CreateSendConfirmationCommand(It.IsAny<Account>()))
                .Returns(vSendConfirmationCommand.Object);


            var vNewAccountCommand = new CreateAccountCommand(
                vObjects.DocumentStore,
                vObjects.HashProvider.Object, vAccountCommandFactory.Object, new CryptoRandomProvider(),
                vNewAccount);
            vNewAccountCommand.Execute();

            var vLogin = new LoginAccount
                             {
                                 Name = cName,
                                 Password = cPassword
                             };
            vObjects.GoodLoginAccountCommand = new LoginAccountCommand(
                vObjects.DocumentStore,
                vObjects.HashProvider.Object,
                vLogin);
            return vObjects;
        }

        [TestCase(cName, cEmail)]
        [TestCase(cEmail, cPassword)]
        public void LoginReturnsFalseWithBadLoginInfo(string tName, string tPassword)
        {
            var vObjects = Setup();
            var vLogin = new LoginAccount
                             {
                                 Name = tName,
                                 Password = tPassword,
                             };
            var vBadLoginAccountCommand = new LoginAccountCommand(
                vObjects.DocumentStore,
                vObjects.HashProvider.Object,
                vLogin);
            var vResult = vBadLoginAccountCommand.Execute();
            Assert.IsFalse(vResult);
        }

        [Test]
        public void GoodLoginUsesHashProviderToRehash()
        {
            var vObjects = Setup();
            vObjects.GoodLoginAccountCommand.Execute();
            vObjects.HashProvider.Verify(t => t.Hash(cPassword));
        }

        [Test]
        public void LoginReturnsTrueWhenGoodLoginInfo()
        {
            var vObjects = Setup();
            var vResult = vObjects.GoodLoginAccountCommand.Execute();
            Assert.IsTrue(vResult);
        }
    }
}