#region LIcense

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

using System.Threading.Tasks;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Security;
using RangoricMUD.Web;
using SignalR.Hubs;

#endregion

namespace RangoricMUD.Accounts.Controllers
{
    public class AccountHub : Hub
    {
        private readonly IAccountCommandFactory mAccountCommandFactory;
        private readonly ISignInPersistance mSignInPersistance;

        public AccountHub(ISignInPersistance tSignInPersistance, IAccountCommandFactory tAccountCommandFactory)
        {
            mSignInPersistance = tSignInPersistance;
            mAccountCommandFactory = tAccountCommandFactory;
        }

        public void Logout()
        {
            mSignInPersistance.Logout(Context.ConnectionId);
        }

        public Task Login(LoginAccount tLoginAccount)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        if (ModelValidator.IsValid(tLoginAccount))
                        {
                            var vCommand =
                                mAccountCommandFactory.CreateLoginAccountCommand(tLoginAccount);
                            if (vCommand.Execute())
                            {
                                mSignInPersistance.Login(tLoginAccount.Name,
                                                         Context.ConnectionId);
                                var vQuery =
                                    mAccountCommandFactory.CreateCheckLoginQuery(Context.ConnectionId);
                                Caller.AddAccount(vQuery.Result);
                            }
                        }
                    });
        }

        public Task CreateAccount(CreateAccount tCreateAccount)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        if (ModelValidator.IsValid(tCreateAccount))
                        {
                            var vCommand =
                                mAccountCommandFactory.CreateCreateAccountCommand(
                                    tCreateAccount);
                            if (vCommand.Execute() == eAccountCreationStatus.Success)
                            {
                                mSignInPersistance.Login(tCreateAccount.Name, Context.ConnectionId);
                                var vQuery =
                                    mAccountCommandFactory.CreateCheckLoginQuery(Context.ConnectionId);
                                Caller.AddAccount(vQuery.Result);
                            }
                        }
                    });
        }

        public Task ConfirmAccount(ConfirmAccountPageModel tConfirmAccountPageModel)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        var vGood = false;

                        if (ModelValidator.IsValid(tConfirmAccountPageModel))
                        {
                            var vCommand =
                                mAccountCommandFactory.CreateLoginAccountCommand(
                                    tConfirmAccountPageModel);

                            if (vCommand.Execute())
                            {
                                var vModel = new ConfirmAccountModel
                                                 {
                                                     Name = tConfirmAccountPageModel.Name,
                                                     ConfirmationNumber =
                                                         tConfirmAccountPageModel
                                                         .ConfirmationNumber
                                                 };
                                var vConfirmCommand =
                                    mAccountCommandFactory.CreateConfirmAccountCommand(vModel);
                                vGood = vConfirmCommand.Execute();
                            }
                        }

                        Caller.AddConfirmation(vGood);
                    });
        }

        public Task CheckLogin()
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        var vQuery =
                            mAccountCommandFactory.CreateCheckLoginQuery(Context.ConnectionId);
                        Caller.AddAccount(vQuery.Result);
                    });
        }
    }
}