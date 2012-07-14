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

using System.Web.Mvc;
using Rangoric.Website.Accounts.Commands;
using Rangoric.Website.Accounts.Models;
using Rangoric.Website.Controllers;
using Rangoric.Website.Security;

#endregion

namespace Rangoric.Website.Accounts.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountCommandFactory mAccountCommandFactory;
        private readonly ISignInPersistance mSignInPersistance;

        public AccountController(ISignInPersistance tSignInPersistance, IAccountCommandFactory tAccountCommandFactory)
        {
            mSignInPersistance = tSignInPersistance;
            mAccountCommandFactory = tAccountCommandFactory;
        }


        [HttpPost]
        public void Logout()
        {
            mSignInPersistance.SignOut();
        }

        [HttpPost]
        public JsonResult Login(ILoginAccount tLoginAccount)
        {
            var vResult = false;
            if (ModelState.IsValid)
            {
                var vCommand = mAccountCommandFactory.CreateLoginAccountCommand(tLoginAccount);
                vResult = vCommand.Execute();
            }
            return Json(vResult, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult CreateAccount(ICreateAccount tCreateAccount)
        {
            var vResult = false;
            if (ModelState.IsValid)
            {
                var vCommand = mAccountCommandFactory.CreateNewAccountCommand(tCreateAccount);
                vResult = vCommand.Execute() == eAccountCreationStatus.Success;
            }
            return Json(vResult, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult CheckLogin()
        {
            var vQuery = mAccountCommandFactory.CreateCheckLoginQuery();
            return Json(
                vQuery.Result,
                JsonRequestBehavior.DenyGet);
        }
    }
}