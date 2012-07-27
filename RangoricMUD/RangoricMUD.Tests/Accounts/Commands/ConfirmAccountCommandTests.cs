using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Tests.Utilities;

namespace RangoricMUD.Tests.Accounts.Commands
{
    [TestFixture]
    public class ConfirmAccountCommandTests: BaseTests
    {
        [TestCase("ABC", "1234567890", "test@email.com", 1)]
        public void SetsConfirmationToTrue(string tName, string tPassword, string tEmail, int tNumber)
        {
            var vDataStore = GetEmbeddedDatabase;
            AddAccountToDataStore(vDataStore, tName, tPassword, tEmail, tNumber);

            var vModel = new ConfirmAccountModel
                             {
                                 Name = tName,
                                 ConfirmationNumber = tNumber
                             };

            var vCommand = new ConfirmAccountCommand(vDataStore, vModel);
            vCommand.Execute();

            using(var vSession =vDataStore.OpenSession())
            {
                var vAccount = vSession.Load<Account>(tName);
                Assert.AreEqual(true, vAccount.IsConfirmed);
            }
        }
        [TestCase("ABC", "1234567890", "test@email.com", 1)]
        public void SetsConfirmationNumberToZero(string tName, string tPassword, string tEmail, int tNumber)
        {
            var vDataStore = GetEmbeddedDatabase;
            AddAccountToDataStore(vDataStore, tName, tPassword, tEmail, tNumber);

            var vModel = new ConfirmAccountModel
            {
                Name = tName,
                ConfirmationNumber = tNumber
            };

            var vCommand = new ConfirmAccountCommand(vDataStore, vModel);
            vCommand.Execute();

            using (var vSession = vDataStore.OpenSession())
            {
                var vAccount = vSession.Load<Account>(tName);
                Assert.AreEqual(0, vAccount.ConfirmationNumber);
            }
        }
    }
}
