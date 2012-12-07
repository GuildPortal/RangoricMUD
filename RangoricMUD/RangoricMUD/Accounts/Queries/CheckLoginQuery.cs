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
using RangoricMUD.Queries;
using RangoricMUD.Security;
using Raven.Client;

#endregion

namespace RangoricMUD.Accounts.Queries
{
    public class CheckLoginQuery : BaseQuery<ICheckLoginModel>, ICheckLoginQuery
    {
        private readonly IDocumentStore mDocumentStore;
        private readonly ISignInPersistance mSignInPersistance;

        public CheckLoginQuery(ISignInPersistance tSignInPersistance, IDocumentStore tDocumentStore)
        {
            mSignInPersistance = tSignInPersistance;
            mDocumentStore = tDocumentStore;
        }

        protected override ICheckLoginModel GetResult()
        {
            var vAccountName = mSignInPersistance.AccountName;
            if (string.IsNullOrWhiteSpace(vAccountName))
            {
                return null;
            }
            Account vAccount;
            using (var vSession = mDocumentStore.OpenSession())
            {
                vAccount = vSession.Load<Account>("Accounts/" + vAccountName);
            }
            var vResult = new CheckLoginModel
                              {
                                  IsLoggedIn = vAccount != null,
                                  IsConfirmed = vAccount.IsConfirmed,
                                  Name = vAccount.Name,
                                  Roles = vAccount.Roles
                              };
            return vResult;
        }
    }
}