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

using NUnit.Framework;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Tests.Utilities;

#endregion

namespace RangoricMUD.Tests.Accounts.Commands
{
    [TestFixture]
    public class ConfirmAccountCommandTests : BaseTests
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

            using (var vSession = vDataStore.OpenSession())
            {
                var vAccount = vSession.Load<Account>("Accounts/" + tName);
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
                var vAccount = vSession.Load<Account>("Accounts/" + tName);
                Assert.AreEqual(0, vAccount.ConfirmationNumber);
            }
        }
    }
}