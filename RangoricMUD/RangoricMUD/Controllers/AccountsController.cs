using System.Threading.Tasks;
using System.Web.Mvc;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Security;
using ModelValidator = RangoricMUD.Web.ModelValidator;

namespace RangoricMUD.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IAccountCommandFactory mAccountCommandFactory;
        private readonly ISignInPersistance mSignInPersistance;

        public AccountsController(ISignInPersistance tSignInPersistance, IAccountCommandFactory tAccountCommandFactory)
        {
            mSignInPersistance = tSignInPersistance;
            mAccountCommandFactory = tAccountCommandFactory;
        }

        public async Task<bool> Login(LoginAccount tLoginAccount)
        {

            if (ModelValidator.IsValid(tLoginAccount))
            {
                var vCommand =
                    mAccountCommandFactory.CreateLoginAccountCommand(tLoginAccount);
                if (await vCommand.Execute())
                {
                    mSignInPersistance.Login(tLoginAccount.Name);
                    return true;
                }
            }
            return false;
        }

        public async Task<ICheckLoginModel> Get()
        {
            var vQuery =
                mAccountCommandFactory.CreateCheckLoginQuery();
            return vQuery.Result;
        }
    }
}