using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RangoricMUD.Characters.Commands;
using RangoricMUD.Characters.Models;
using RangoricMUD.Characters.Queries;
using RangoricMUD.Tests.Utilities;

namespace RangoricMUD.Tests.Characters.Queries
{
    [TestFixture]
    public class GetAllCharactersForAccountQueryTests : BaseTests
    {
        private const string cGameName = "Game Name";
        private const string cAccountName = "Account Name";
        [TestCase("ABC", "ABCD")]
        public void GetAllCharactersForAccountWorksToGetOne(string tName, string tListName)
        {
            var vDocumentStore = GetEmbeddedDatabase;

            AddAccountToDataStore(vDocumentStore, cAccountName, "ABC", "test@email.com");

            var vModel = new CreateCharacterModel
            {
                GameName = cGameName,
                ListName = tListName,
                Name = tName,
            };

            var vCommand = new CreateCharacterCommand(vDocumentStore, vModel, cAccountName);
            vCommand.Execute();

            var vQuery = new GetAllCharactersForAccountQuery(vDocumentStore, cGameName, cAccountName);
            var vResults = vQuery.Result;

            Assert.AreEqual(1, vResults.Count);
        }
    }
}
