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
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Commands;
using RangoricMUD.Security;
using Raven.Abstractions.Exceptions;
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
            using (var vSession = mDocumentStore.OpenSession())
            {
                vSession.Advanced.UseOptimisticConcurrency = true;
                var vAccount = new Account
                                   {
                                       Name = mCreateAccount.Name,
                                       Email = mCreateAccount.Email,
                                       PasswordHash = mHashProvider.Hash(mCreateAccount.Password),
                                       Roles = new List<eRoles> {eRoles.Player}
                                   };

                vSession.Store(vAccount, vAccount.Name);

                try
                {
                    vSession.SaveChanges();
                }
                catch (ConcurrencyException)
                {
                    return eAccountCreationStatus.DuplicateName;
                }
            }

            return eAccountCreationStatus.Success;
        }

        #endregion
    }
}