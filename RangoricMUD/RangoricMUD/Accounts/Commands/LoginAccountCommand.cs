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

using System.Linq;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Commands;
using RangoricMUD.Security;
using Raven.Client;

#endregion

namespace RangoricMUD.Accounts.Commands
{
    public class LoginAccountCommand : BaseCommand<bool>, ILoginAccountCommand
    {
        private readonly IDocumentStore mDocumentStore;
        private readonly IHashProvider mHashProvider;
        private readonly LoginAccount mLogin;

        public LoginAccountCommand(IDocumentStore tDocumentStore,
                                   IHashProvider tHashProvider, LoginAccount tLoginAccount)
        {
            mDocumentStore = tDocumentStore;
            mHashProvider = tHashProvider;
            mLogin = tLoginAccount;
        }

        #region ILoginAccountCommand Members

        public override bool Execute()
        {
            Account vAccount;
            using (var vSession = mDocumentStore.OpenSession())
            {
                vAccount =
                    vSession.Query<Account>().SingleOrDefault(t => t.Name == mLogin.Name);
                if (vAccount == null)
                {
                    return false;
                }
            }

            var vGood = mHashProvider.CheckHash(mLogin.Password, vAccount.PasswordHash);
            return vGood;
        }

        #endregion
    }
}