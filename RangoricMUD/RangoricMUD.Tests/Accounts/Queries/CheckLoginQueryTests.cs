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
using RangoricMUD.Accounts.Queries;
using RangoricMUD.Security;
using RangoricMUD.Tests.Utilities;
using Raven.Client.Embedded;

#endregion

namespace RangoricMUD.Tests.Accounts.Queries
{
    [TestFixture]
    public class CheckLoginQueryTests
    {
        private class TestObjects
        {
            public EmbeddableDocumentStore DocumentStore;
            public CheckLoginQuery Query;
            public Mock<ISignInPersistance> SignInPersistance;
        }

        private string cTestName = "Test Name";

        private TestObjects Setup()
        {
            var vReturn = new TestObjects();
            vReturn.SignInPersistance = new Mock<ISignInPersistance>();

            vReturn.DocumentStore = new EmbeddableDocumentStore {RunInMemory = true};
            vReturn.DocumentStore.Initialize();
            vReturn.DocumentStore.RegisterListener(new RavenDbNoStaleData());

            //Make sure there is an account to get. Since this deals with getting an account.
            var vNewAccount = new CreateAccountCommand(vReturn.DocumentStore, new CustomHashProvider(),
                                                    new CreateAccount
                                                        {
                                                            Email = "test@email.com",
                                                            Name = cTestName,
                                                            Password = "Test Password"
                                                        });
            vNewAccount.Execute();

            vReturn.Query = new CheckLoginQuery(vReturn.SignInPersistance.Object, vReturn.DocumentStore, "Test");
            return vReturn;
        }

        [Test]
        public void CanGetLoggedInInformationWhenNameIsRight()
        {
            var vObjects = Setup();
            vObjects.SignInPersistance.Setup(t => t.AccountName("Test")).Returns(cTestName);
            var vResult = vObjects.Query.Result;
            Assert.IsTrue(vResult.IsLoggedIn);
            Assert.AreEqual(vResult.Name, cTestName);
        }

        [Test]
        public void DefaultRoleOfPlayerIsGotten()
        {
            var vObjects = Setup();
            vObjects.SignInPersistance.Setup(t => t.AccountName("Test")).Returns(cTestName);
            var vResult = vObjects.Query.Result;
            Assert.AreEqual(eRoles.Player, vResult.Roles.First());
        }

        [Test]
        public void MinimumDependencies()
        {
            Setup();
        }

        [Test]
        public void ReturnsFalseIfNooneIsLoggedIn()
        {
            var vObjects = Setup();
            var vResult = vObjects.Query.Result;
            Assert.IsNull(vResult);
        }

        [Test]
        public void UsesSignInPersistanceToGetAccountName()
        {
            var vObjects = Setup();
            var vResult = vObjects.Query.Result;
            vObjects.SignInPersistance.Verify(t => t.AccountName("Test"));
        }
    }
}