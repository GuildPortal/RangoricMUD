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

using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Commands;
using Raven.Client;

#endregion

namespace RangoricMUD.Accounts.Commands
{
    public class ConfirmAccountCommand : BaseCommand<bool>, IConfirmAccountCommand
    {
        private readonly ConfirmAccountModel mConfirmAccountModel;

        private readonly IDocumentStore mDocumentStore;

        public ConfirmAccountCommand(IDocumentStore tDocumentStore, ConfirmAccountModel tConfirmAccountModel)
        {
            mDocumentStore = tDocumentStore;
            mConfirmAccountModel = tConfirmAccountModel;
        }

        #region IConfirmAccountCommand Members

        public override bool Execute()
        {
            bool vGood;
            if (mConfirmAccountModel.ConfirmationNumber == 0)
            {
                return false;
            }
            using (var vSession = mDocumentStore.OpenSession())
            {
                var vAccount = new Account() {Name = mConfirmAccountModel.Name};
                vAccount = vSession.Load<Account>(vAccount.Id);
                vGood = vAccount.ConfirmationNumber == mConfirmAccountModel.ConfirmationNumber;
                if (vGood)
                {
                    vAccount.IsConfirmed = true;
                    vAccount.ConfirmationNumber = 0;
                    vSession.Store(vAccount);
                    vSession.SaveChanges();
                }
            }

            return vGood;
        }

        #endregion
    }
}