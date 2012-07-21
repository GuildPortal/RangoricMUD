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
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Crews;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Bootstrappers;
using RangoricMUD.Bootstrappers.Crews;
using RangoricMUD.Tests.Utilities;
using Raven.Client;
using Raven.Client.Embedded;

#endregion

namespace RangoricMUD.Tests.Accounts.Crews
{
    [TestFixture]
    public class AccountCrewTests : BaseTests
    {
        private WindsorShip mShip;
        private IDocumentStore mDocumentStore;

        [TestFixtureSetUp]
        public void Setup()
        {
            mShip = new WindsorShip();
            mShip.Crew.Add(new DiceCrew());
            mShip.Crew.Add(new SecurityCrew());

            mDocumentStore = GetEmbeddedDatabase;
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

        [TestCase(typeof (IAccountCommandFactory))]
        public void ServicesResolve(Type tType)
        {
            TestService(tType);
        }

        [Test]
        public void AccountCommandFactoryCanGenerateLoginAccountCommand()
        {
            var vFactory = mShip.GetService(typeof (IAccountCommandFactory)) as IAccountCommandFactory;
            var vLoginAccountCommand = vFactory.CreateLoginAccountCommand(new LoginAccount(), "");
            Assert.IsNotNull(vLoginAccountCommand);
        }

        [Test]
        public void AccountCommandFactoryCanGenerateNewAccountCommand()
        {
            var vFactory = mShip.GetService(typeof (IAccountCommandFactory)) as IAccountCommandFactory;
            var vNewAccountCommand = vFactory.CreateCreateAccountCommand(new CreateAccount());
            Assert.IsNotNull(vNewAccountCommand);
        }
    }
}