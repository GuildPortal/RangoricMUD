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
using System.Configuration;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Commands;
using RangoricMUD.Dice;
using RangoricMUD.Security;
using Raven.Abstractions.Exceptions;
using Raven.Client;

#endregion

namespace RangoricMUD.Accounts.Commands
{
    /// <summary>
    ///   Creates and account in a ravendb document store.
    ///   Will make the account as a player automaically, and add admin
    ///   if the name and email match the correct settings in the web.config
    /// </summary>
    public class CreateAccountCommand : BaseCommand<eAccountCreationStatus>, ICreateAccountCommand
    {
        private readonly IAccountCommandFactory mAccountCommandFactory;
        private readonly CreateAccount mCreateAccount;
        private readonly IDocumentStore mDocumentStore;
        private readonly IHashProvider mHashProvider;
        private readonly IRandomProvider mRandomProvider;

        /// <summary>
        ///   Creates an instance of this command.
        /// </summary>
        /// <param name="tDocumentStore"> The RavenDB Document Store </param>
        /// <param name="tHashProvider"> A hash provider to make sure we srore no passwords </param>
        /// <param name="tAccountCommandFactory"> </param>
        /// <param name="tRandomProvider"> How we generate the confirmation number </param>
        /// <param name="tCreateAccount"> The data needed to create the account </param>
        public CreateAccountCommand(IDocumentStore tDocumentStore, IHashProvider tHashProvider,
                                    IAccountCommandFactory tAccountCommandFactory, IRandomProvider tRandomProvider,
                                    CreateAccount tCreateAccount)
        {
            mDocumentStore = tDocumentStore;
            mHashProvider = tHashProvider;
            mAccountCommandFactory = tAccountCommandFactory;
            mRandomProvider = tRandomProvider;
            mCreateAccount = tCreateAccount;
        }

        #region ICreateAccountCommand Members

        /// <summary>
        ///   Actually creates the account and returns the result.
        /// </summary>
        /// <returns> Success when the account is created. Duplicate Name when the name is already in use. </returns>
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
                                       IsConfirmed = false,
                                       ConfirmationNumber = mRandomProvider.GetInteger(100000, 2000000000),
                                       Roles = new List<eRoles> {eRoles.Player}
                                   };

                if (IsAdmin())
                {
                    vAccount.Roles.Add(eRoles.Admin);
                }
                vSession.Store(vAccount, vAccount.Name);

                try
                {
                    vSession.SaveChanges();
                }
                catch (ConcurrencyException)
                {
                    return eAccountCreationStatus.DuplicateName;
                }
                var vCommand = mAccountCommandFactory.CreateSendConfirmationCommand(vAccount);
                if (!vCommand.Execute())
                {
                    return eAccountCreationStatus.ConfirmationEmailFailed;
                }
            }

            return eAccountCreationStatus.Success;
        }

        #endregion

        private bool IsAdmin()
        {
            var vAdminName = ConfigurationManager.AppSettings["Accounts/Admin/Name"];
            var vAdminEmail = ConfigurationManager.AppSettings["Accounts/Admin/Email"];

            return
                mCreateAccount.Name == vAdminName &&
                mCreateAccount.Email == vAdminEmail;
        }
    }
}