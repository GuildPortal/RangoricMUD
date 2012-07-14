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

using System;
using NUnit.Framework;
using Rangoric.UnitTests.Utilities;
using Rangoric.Website.Accounts.Commands;
using Rangoric.Website.Accounts.Controllers;
using Rangoric.Website.Accounts.Crews;
using Rangoric.Website.Accounts.Data;
using Rangoric.Website.Accounts.Models;
using Rangoric.Website.Bootstrappers;
using Rangoric.Website.Bootstrappers.Crews;
using Raven.Client.Embedded;

#endregion

namespace Rangoric.UnitTests.Accounts.Crews
{
    [TestFixture]
    public class AccountCrewsTests : IDisposable
    {
        private WindsorShip mShip;
        private EmbeddableDocumentStore mDocumentStore;

        [TestFixtureSetUp]
        public void Setup()
        {
            mShip = new WindsorShip();
            mShip.Crew.Add(new DiceCrew());
            mShip.Crew.Add(new SecurityCrew());

            mDocumentStore = new EmbeddableDocumentStore {RunInMemory = true};
            mDocumentStore.Initialize();
            mDocumentStore.RegisterListener(new RavenDbNoStaleData());
            mShip.Crew.Add(new DatabaseCrew(mDocumentStore));

            mShip.Crew.Add(new AccountCrew());
            mShip.SetSail();
        }

        private void TestService(Type tType)
        {
            var vResult = mShip.GetService(tType);
            Assert.IsNotNull(vResult);
            mShip.ReleaseService(vResult);
        }

        [TestCase(typeof (IAccount))]
        [TestCase(typeof (ILoginAccount))]
        [TestCase(typeof (ICreateAccount))]
        [TestCase(typeof (IAccountCommandFactory))]
        [TestCase(typeof (AccountController))]
        public void ServicesResolve(Type tType)
        {
            TestService(tType);
        }

        protected virtual void Dispose(bool tManaged)
        {
            mShip.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Test]
        public void AccountCommandFactoryCanGenerateLoginAccountCommand()
        {
            var vFactory = mShip.GetService(typeof (IAccountCommandFactory)) as IAccountCommandFactory;
            var vLoginAccountCommand = vFactory.CreateLoginAccountCommand(new LoginAccount());
            Assert.IsNotNull(vLoginAccountCommand);
        }

        [Test]
        public void AccountCommandFactoryCanGenerateNewAccountCommand()
        {
            var vFactory = mShip.GetService(typeof (IAccountCommandFactory)) as IAccountCommandFactory;
            var vNewAccountCommand = vFactory.CreateNewAccountCommand(new CreateAccount());
            Assert.IsNotNull(vNewAccountCommand);
        }
    }
}