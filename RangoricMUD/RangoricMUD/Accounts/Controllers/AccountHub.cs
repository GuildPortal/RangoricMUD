using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Security;
using RangoricMUD.Web;
using SignalR.Hubs;

namespace RangoricMUD.Accounts.Controllers
{
    public class AccountHub : Hub
    {
        private readonly ISignInPersistance mSignInPersistance;
        private readonly IAccountCommandFactory mAccountCommandFactory;

        public AccountHub(ISignInPersistance tSignInPersistance, IAccountCommandFactory tAccountCommandFactory)
        {
            mSignInPersistance = tSignInPersistance;
            mAccountCommandFactory = tAccountCommandFactory;
        }

        public void Logout()
        {
            mSignInPersistance.Logout(Context.ConnectionId);
        }
        public Task<bool> Login(LoginAccount tLoginAccount)
        {
            return Task.Factory.StartNew(() =>
                                             {
                                                 var vResult = false;
                                                 if (ModelValidator.IsValid(tLoginAccount))
                                                 {
                                                     var vCommand =
                                                         mAccountCommandFactory.CreateLoginAccountCommand(tLoginAccount);
                                                     vResult = vCommand.Execute();
                                                     if(vResult)
                                                     {
                                                         mSignInPersistance.Login(tLoginAccount.Name,
                                                                                  Context.ConnectionId);
                                                     }
                                                 }
                                                 return vResult;
                                             });

        }

        public Task<eAccountCreationStatus> CreateAccount(CreateAccount tCreateAccount)
        {
            return Task.Factory.StartNew(() =>
                                             {
                                                 if (ModelValidator.IsValid(tCreateAccount))
                                                 {
                                                     var vCommand =
                                                         mAccountCommandFactory.CreateCreateAccountCommand(
                                                             tCreateAccount);
                                                     return vCommand.Execute();
                                                 }
                                                 return eAccountCreationStatus.InvalidModel;
                                             });
        }

        public Task<ICheckLoginModel> CheckLogin()
        {
            return Task.Factory.StartNew(() =>
                                             {
                                                 var vQuery =
                                                     mAccountCommandFactory.CreateCheckLoginQuery(Context.ConnectionId);
                                                 return vQuery.Result;
                                             });
        }
    }
}