// LoginAccountCommandTests.cs is a part of Rangoric and covered by the Open Gaming License
// available to be viewed at: https://bitbucket.org/Rangoric/rangoric
// 
// The original source code of this file are to be considered OGC.
// 
// Copyright 2012: Wilson Mead III

using Moq;
using NUnit.Framework;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Security;
using RangoricMUD.Tests.Utilities;
using Raven.Client.Embedded;

namespace RangoricMUD.Tests.Accounts.Commands
{
    [TestFixture]
    public class LoginAccountCommandTests
    {
        private class TestObjects
        {
            public Mock<IHashProvider> HashProvider;
            public Mock<ISignInPersistance> SignInPersistance;
            public EmbeddableDocumentStore DocumentStore;
            public LoginAccountCommand GoodLoginAccountCommand;
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

            vObjects.SignInPersistance = new Mock<ISignInPersistance>();

            vObjects.DocumentStore = new EmbeddableDocumentStore { RunInMemory = true };
            vObjects.DocumentStore.Initialize();
            vObjects.DocumentStore.RegisterListener(new RavenDbNoStaleData());
            var vNewAccount = new CreateAccount
                              {
                                  Name = cName,
                                  Email = cEmail,
                                  Password = cPassword
                              };
            var vNewAccountCommand = new NewAccountCommand(
                vObjects.DocumentStore,
                vObjects.HashProvider.Object,
                vNewAccount);
            vNewAccountCommand.Execute();

            var vLogin = new LoginAccount
                         {
                             Name = cName,
                             Password = cPassword
                         };
            vObjects.GoodLoginAccountCommand = new LoginAccountCommand(
                vObjects.SignInPersistance.Object,
                vObjects.DocumentStore,
                vObjects.HashProvider.Object,
                vLogin);
            return vObjects;
        }

        [Test]
        public void GoodLoginSetsUpSignInPersistance() {
            var vObjects = Setup();
            vObjects.GoodLoginAccountCommand.Execute();
            vObjects.SignInPersistance.VerifySet(t => t.AccountName = cName);
        }

        [Test]
        public void GoodLoginUsesHashProviderToRehash() {
            var vObjects = Setup();
            vObjects.GoodLoginAccountCommand.Execute();
            vObjects.HashProvider.Verify(t => t.Hash(cPassword));
        }

        [Test]
        [TestCase(cName, cEmail)]
        [TestCase(cEmail, cPassword)]
        public void LoginReturnsFalseWithBadLoginInfo(string tName, string tPassword) {
            var vObjects = Setup();
            var vLogin = new LoginAccount
                         {
                             Name = tName,
                             Password = tPassword,
                         };
            var vBadLoginAccountCommand = new LoginAccountCommand(
                vObjects.SignInPersistance.Object,
                vObjects.DocumentStore,
                vObjects.HashProvider.Object,
                vLogin);
            var vResult = vBadLoginAccountCommand.Execute();
            Assert.IsFalse(vResult);
        }

        [Test]
        public void LoginReturnsTrueWhenGoodLoginInfo() {
            var vObjects = Setup();
            var vResult = vObjects.GoodLoginAccountCommand.Execute();
            Assert.IsTrue(vResult);
        }
    }
}