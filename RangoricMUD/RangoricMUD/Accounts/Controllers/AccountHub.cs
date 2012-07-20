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
                                                         mAccountCommandFactory.CreateLoginAccountCommand(tLoginAccount, Context.ConnectionId);
                                                     vResult = vCommand.Execute();
                                                 }
                                                 return vResult;
                                             });

        }

        public void CreateAccount(CreateAccount tCreateAccount)
        {
            var vResult = false;
            if(ModelValidator.IsValid(tCreateAccount))
            {
                var vCommand = mAccountCommandFactory.CreateCreateAccountCommand(tCreateAccount);
                vResult = vCommand.Execute() == eAccountCreationStatus.Success;
            }
            Caller.CreateAccountResult(vResult);
        }

        public void CheckLogin()
        {
            
            var vQuery = mAccountCommandFactory.CreateCheckLoginQuery(Context.ConnectionId);
            Caller.AccountInfo(vQuery.Result);
        }
    }
}