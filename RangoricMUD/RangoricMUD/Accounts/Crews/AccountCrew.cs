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

using Rangoric.Website.Accounts.Commands;
using Rangoric.Website.Accounts.Controllers;
using Rangoric.Website.Accounts.Data;
using Rangoric.Website.Accounts.Models;
using Rangoric.Website.Accounts.Queries;
using Rangoric.Website.Bootstrappers.Crews;

#endregion

namespace Rangoric.Website.Accounts.Crews
{
    public class AccountCrew : BaseCrew
    {
        protected override void StrapOn()
        {
            Add().OfInterface<IAccount>().WithImplementation<Account>();
            Add().OfInterface<AccountController>().WithImplementation<AccountController>();
            Add().OfInterface<ILoginAccount>().WithImplementation<LoginAccount>();
            Add().OfInterface<ILoginAccountCommand>().WithImplementation<LoginAccountCommand>();
            Add().OfInterface<ICreateAccount>().WithImplementation<CreateAccount>();
            Add().OfInterface<INewAccountCommand>().WithImplementation<NewAccountCommand>();
            Add().OfInterface<IAccountCommandFactory>().AsFactory();
            Add().OfInterface<ICheckLoginQuery>().WithImplementation<CheckLoginQuery>();
        }
    }
}