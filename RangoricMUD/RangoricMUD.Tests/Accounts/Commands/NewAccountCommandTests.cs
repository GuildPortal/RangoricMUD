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
using Rangoric.UnitTests.Utilities;
using Rangoric.Website.Accounts.Commands;
using Rangoric.Website.Accounts.Models;
using Rangoric.Website.Security;
using Raven.Client.Embedded;

#endregion

namespace Rangoric.UnitTests.Accounts.Commands
{
    [TestFixture]
    public class NewAccountCommandTests
    {
        private const string cHash = "Hash";
        private const string cName = "Name";
        private const string cEmail = "Email";
        private const string cPassword = "Password";
        private INewAccountCommand mNewAccountCommand;
        private ICreateAccount mCreateAccount;
        private Mock<IHashProvider> mHashProvider;
        private EmbeddableDocumentStore mDocumentStore;

        private void Setup()
        {
            mHashProvider = new Mock<IHashProvider>();
            mHashProvider.Setup(t => t.Hash(cPassword)).Returns(cHash);

            mDocumentStore = new EmbeddableDocumentStore {RunInMemory = true};
            mDocumentStore.Initialize();
            mDocumentStore.RegisterListener(new RavenDbNoStaleData());
            mCreateAccount = new CreateAccount
                                 {
                                     Name = cName,
                                     Email = cEmail,
                                     Password = cPassword
                                 };
            mNewAccountCommand = new NewAccountCommand(
                mDocumentStore,
                mHashProvider.Object,
                mCreateAccount);
        }

        [Test]
        public void ExecuteReturnsDuplicateWhenDuplicateName()
        {
            Setup();
            mNewAccountCommand.Execute();
            var vResult = mNewAccountCommand.Execute();
            Assert.AreEqual(eAccountCreationStatus.DuplicateName, vResult);
        }

        [Test]
        public void ExecuteReturnsSuccessWhenGood()
        {
            Setup();
            var vResult = mNewAccountCommand.Execute();
            Assert.AreEqual(eAccountCreationStatus.Success, vResult);
        }

        [Test]
        public void ExecuteUsesHashProviderForHashWithSalt()
        {
            Setup();
            mNewAccountCommand.Execute();
            mHashProvider.Verify(t => t.Hash(cPassword));
        }

        [Test]
        public void MinimumDependencies()
        {
            Setup();
        }
    }
}