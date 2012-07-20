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

using System.Collections.Generic;
using System.Linq;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Commands;
using RangoricMUD.Security;
using Raven.Client;

#endregion

namespace RangoricMUD.Accounts.Commands
{
    public class CreateAccountCommand : BaseCommand<eAccountCreationStatus>, ICreateAccountCommand
    {
        private readonly CreateAccount mCreateAccount;
        private readonly IDocumentStore mDocumentStore;
        private readonly IHashProvider mHashProvider;

        public CreateAccountCommand(IDocumentStore tDocumentStore, IHashProvider tHashProvider,
                                 CreateAccount tCreateAccount)
        {
            mDocumentStore = tDocumentStore;
            mHashProvider = tHashProvider;
            mCreateAccount = tCreateAccount;
        }

        #region ICreateAccountCommand Members

        public override eAccountCreationStatus Execute()
        {
            var vSession = mDocumentStore.OpenSession();
            var vAccount = vSession.Query<Account>().SingleOrDefault(t => t.Name == mCreateAccount.Name);
            if (vAccount != null)
            {
                return eAccountCreationStatus.DuplicateName;
            }

            vAccount = new Account
                           {
                               Name = mCreateAccount.Name,
                               Email = mCreateAccount.Email,
                               PasswordHash = mHashProvider.Hash(mCreateAccount.Password),
                               Roles = new List<eRoles> {eRoles.Player}
                           };
            vSession.Store(vAccount);
            vSession.SaveChanges();
            vSession.Dispose();

            return eAccountCreationStatus.Success;
        }

        #endregion
    }
}