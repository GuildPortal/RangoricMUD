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
using RangoricMUD.Accounts.Controllers;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Accounts.Queries;
using RangoricMUD.Security;

#endregion

namespace RangoricMUD.Tests.Accounts.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private const string cName = "Login Name";

        private AccountController mAccountController;

        private LoginAccount mGoodLoginAccount;
        private LoginAccount mBadLoginAccount;
        private CreateAccount mGoodNewAccount;
        private CreateAccount mBadNewAccount;

        private Mock<ISignInPersistance> mSignInPersistance;
        private Mock<IAccountCommandFactory> mCommandFactory;
        private Mock<INewAccountCommand> mGoodNewAccountCommand;
        private Mock<INewAccountCommand> mBadNewAccountCommand;
        private Mock<ILoginAccountCommand> mGoodLoginCommand;
        private Mock<ILoginAccountCommand> mBadLoginCommand;
        private Mock<ICheckLoginQuery> mGoodCheckLogin;
        private Mock<ICheckLoginQuery> mBadCheckLogin;

        private void Setup()
        {
            mGoodLoginAccount = new LoginAccount();
            mBadLoginAccount = new LoginAccount();

            mGoodLoginCommand = new Mock<ILoginAccountCommand>();
            mBadLoginCommand = new Mock<ILoginAccountCommand>();
            mGoodLoginCommand.Setup(t => t.Execute()).Returns(true);
            mBadLoginCommand.Setup(t => t.Execute()).Returns(false);

            mGoodNewAccount = new CreateAccount();
            mBadNewAccount = new CreateAccount();

            mSignInPersistance = new Mock<ISignInPersistance>();

            mGoodNewAccountCommand = new Mock<INewAccountCommand>();
            mGoodNewAccountCommand.Setup(t => t.Execute()).Returns(eAccountCreationStatus.Success);
            mBadNewAccountCommand = new Mock<INewAccountCommand>();
            mBadNewAccountCommand.Setup(t => t.Execute()).Returns(eAccountCreationStatus.DuplicateName);

            mGoodCheckLogin = new Mock<ICheckLoginQuery>();
            mGoodCheckLogin.SetupGet(t => t.Result).Returns(new CheckLoginModel {IsLoggedIn = true});
            mBadCheckLogin = new Mock<ICheckLoginQuery>();
            mBadCheckLogin.SetupGet(t => t.Result).Returns(new CheckLoginModel());

            mCommandFactory = new Mock<IAccountCommandFactory>();
            mCommandFactory
                .Setup(t => t.CreateNewAccountCommand(mGoodNewAccount))
                .Returns(mGoodNewAccountCommand.Object);
            mCommandFactory
                .Setup(t => t.CreateNewAccountCommand(mBadNewAccount))
                .Returns(mBadNewAccountCommand.Object);
            mCommandFactory
                .Setup(t => t.CreateLoginAccountCommand(mGoodLoginAccount))
                .Returns(mGoodLoginCommand.Object);
            mCommandFactory
                .Setup(t => t.CreateLoginAccountCommand(mBadLoginAccount))
                .Returns(mBadLoginCommand.Object);

            mAccountController = new AccountController(mSignInPersistance.Object, mCommandFactory.Object);
        }

        [Test]
        public void CheckLoginIsFalseWhenNotLoggedIn()
        {
            Setup();
            mCommandFactory
                .Setup(t => t.CreateCheckLoginQuery())
                .Returns(mBadCheckLogin.Object);
            dynamic vResult = mAccountController.CheckLogin().Data;
            Assert.IsFalse((bool) vResult.IsLoggedIn);
        }

        [Test]
        public void CheckLoginTrueIfLoggedIn()
        {
            Setup();
            mCommandFactory
                .Setup(t => t.CreateCheckLoginQuery())
                .Returns(mGoodCheckLogin.Object);
            dynamic vResult = mAccountController.CheckLogin().Data;
            Assert.IsTrue((bool) vResult.IsLoggedIn);
        }

        [Test]
        public void CheckLoginUsesPersistanceService()
        {
            Setup();
            mCommandFactory
                .Setup(t => t.CreateCheckLoginQuery())
                .Returns(mGoodCheckLogin.Object);
            mAccountController.CheckLogin();
            mGoodCheckLogin.VerifyGet(t => t.Result);
        }

        [Test]
        public void LoginCanLoginIfLoginAccountIsCorrect()
        {
            Setup();
            Assert.IsTrue((bool) mAccountController.Login(mGoodLoginAccount).Data);
        }

        [Test]
        public void LoginCantLoginIfLoginAccountIsBad()
        {
            Setup();
            Assert.IsFalse((bool) mAccountController.Login(mBadLoginAccount).Data);
        }


        [Test]
        public void LoginUsesLoginCommand()
        {
            Setup();
            mAccountController.Login(mGoodLoginAccount);
            mGoodLoginCommand.Verify(t => t.Execute());
        }


        [Test]
        public void MinimumDependencies()
        {
            Setup();
        }


        [Test]
        public void RegisterReturnsDuplicateNameWhenNewAccountIsDup()
        {
            Setup();
            Assert.AreEqual(false, mAccountController.CreateAccount(mBadNewAccount).Data);
        }

        [Test]
        public void RegisterReturnsSuccessWhenNewAccountIsGood()
        {
            Setup();
            Assert.AreEqual(true, mAccountController.CreateAccount(mGoodNewAccount).Data);
        }
    }
}