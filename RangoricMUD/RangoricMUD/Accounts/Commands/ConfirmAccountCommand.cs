using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Commands;
using Raven.Client;

namespace RangoricMUD.Accounts.Commands
{
    public class ConfirmAccountCommand: BaseCommand<bool>, IConfirmAccountCommand
    {
        public ConfirmAccountCommand(IDocumentStore tDocumentStore, ConfirmAccountModel tConfirmAccountModel)
        {
            mDocumentStore = tDocumentStore;
            mConfirmAccountModel = tConfirmAccountModel;
        }

        private readonly ConfirmAccountModel mConfirmAccountModel;

        private readonly IDocumentStore mDocumentStore;

        public override bool Execute()
        {
            bool vGood;
            if (mConfirmAccountModel.ConfirmationNumber == 0)
            {
                return false;
            }
            using (var vSession = mDocumentStore.OpenSession())
            {
                var vAccount = vSession.Load<Account>(mConfirmAccountModel.Name);
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
    }
}