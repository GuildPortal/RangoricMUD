using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Characters.Data;
using RangoricMUD.Queries;
using Raven.Client;

namespace RangoricMUD.Characters.Queries
{
    public class GetAllCharactersForAccountQuery : IGetAllCharactersForAccountQuery
    {
        private readonly IDocumentStore mDocumentStore;
        private readonly string mGameName;
        private readonly string mAccountName;

        public GetAllCharactersForAccountQuery(IDocumentStore tDocumentStore, string tGameName, string tAccountName)
        {
            mDocumentStore = tDocumentStore;
            mGameName = tGameName;
            mAccountName = tAccountName;
        }

        public List<Character> Result
        {
            get
            {
                var vReturn = new List<Character>();
                using(var vSession = mDocumentStore.OpenSession())
                {
                    var vAccount = new Account() {Name = mAccountName};
                    vAccount = vSession.Include<Account>(t => t.Characters).Load<Account>(vAccount.Id);

                    foreach(var vCharacter in vAccount.Characters)
                    {
                        vReturn.Add(vSession.Load<Character>(vCharacter));
                    }
                }
                return vReturn;
            }
        }
    }
    public interface IGetAllCharactersForAccountQuery : IQuery<List<Character>> 
    {
        
    }
}