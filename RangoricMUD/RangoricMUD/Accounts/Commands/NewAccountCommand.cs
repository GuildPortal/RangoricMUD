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
using Rangoric.Website.Accounts.Data;
using Rangoric.Website.Accounts.Models;
using Rangoric.Website.Commands;
using Rangoric.Website.Security;
using Raven.Client;

#endregion

namespace Rangoric.Website.Accounts.Commands
{
    public class NewAccountCommand : BaseCommand<eAccountCreationStatus>, INewAccountCommand
    {
        private readonly ICreateAccount mCreateAccount;
        private readonly IDocumentStore mDocumentStore;
        private readonly IHashProvider mHashProvider;

        public NewAccountCommand(IDocumentStore tDocumentStore, IHashProvider tHashProvider,
                                 ICreateAccount tCreateAccount)
        {
            mDocumentStore = tDocumentStore;
            mHashProvider = tHashProvider;
            mCreateAccount = tCreateAccount;
        }

        #region INewAccountCommand Members

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