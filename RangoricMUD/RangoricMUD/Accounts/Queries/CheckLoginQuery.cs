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
using RangoricMUD.Queries;
using RangoricMUD.Security;
using Raven.Client;

#endregion

namespace RangoricMUD.Accounts.Queries
{
    public class CheckLoginQuery : BaseQuery<ICheckLoginModel>, ICheckLoginQuery
    {
        private readonly IDocumentStore mDocumentStore;
        private readonly string mConnectionID;
        private readonly ISignInPersistance mSignInPersistance;

        public CheckLoginQuery(ISignInPersistance tSignInPersistance, IDocumentStore tDocumentStore, string tConnectionID)
        {
            mSignInPersistance = tSignInPersistance;
            mDocumentStore = tDocumentStore;
            mConnectionID = tConnectionID;
        }

        protected override ICheckLoginModel GetResult()
        {
            var vAccountName = mSignInPersistance.AccountName(mConnectionID);

            Account vAccount;
            using (var vSession = mDocumentStore.OpenSession())
            {
                vAccount =
                    vSession.Query<Account>().SingleOrDefault(t => t.Name == vAccountName);
            }
            var vResult = new CheckLoginModel
                              {
                                  IsLoggedIn = !string.IsNullOrWhiteSpace(vAccountName),
                                  Name = vAccount != null ? vAccount.Name : "",
                                  Roles = vAccount != null ? vAccount.Roles : new List<eRoles>()
                              };
            return vResult;
        }
    }
}