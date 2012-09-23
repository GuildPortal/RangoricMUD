#region LIcense

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RangoricMUD.Accounts.Commands;
using RangoricMUD.Accounts.Data;
using RangoricMUD.Accounts.Models;
using RangoricMUD.Characters.Commands;
using RangoricMUD.Characters.Data;
using RangoricMUD.Characters.Models;
using RangoricMUD.Dice;
using RangoricMUD.Security;
using RangoricMUD.Tests.Utilities;

#endregion

namespace RangoricMUD.Tests.Characters.Commands
{
    [TestFixture]
    public class CreateCharacterCommandTests : BaseTests
    {
        /// <summary>
        /// User Name is constant because we aren't worried about that here.
        /// </summary>
        private const string cUserName = "ABC";

        /// <summary>
        /// Game Name is constant because we aren't worried about that here.
        /// </summary>
        private const string cGameName = "ABC";

        [TestCase("ABC", "ABC")]
        public void CanExecuteCommand(string tListName, string tName)
        {
            var vDocumentStore = GetEmbeddedDatabase;

            AddAccountToDataStore(vDocumentStore, cUserName, "ABC", "test@email.com");
            var vModel = new CreateCharacterModel
                             {
                                 GameName = cGameName,
                                 ListName = tListName,
                                 Name = tName,
                             };

            var vCommand = new CreateCharacterCommand(vDocumentStore, vModel, cUserName);

            Assert.AreEqual(eCharacterCreationStatus.Success, vCommand.Execute());
        }

        [TestCase("ABC", "ABCD")]
        public void CharacterIsInDatabase(string tListName, string tName)
        {
            var vDocumentStore = GetEmbeddedDatabase;

            AddAccountToDataStore(vDocumentStore, cUserName, "ABC", "test@email.com");
            var vModel = new CreateCharacterModel
                             {
                                 GameName = cGameName,
                                 ListName = tListName,
                                 Name = tName,
                             };

            var vCommand = new CreateCharacterCommand(vDocumentStore, vModel, cUserName);

            Assert.AreEqual(eCharacterCreationStatus.Success, vCommand.Execute());

            using (var vSession = vDocumentStore.OpenSession())
            {
                Assert.IsNotNull(
                    vSession.Load<Character>("Games/" + cGameName + "/" + cUserName + "/Characters/" + tListName));
            }
        }
        [TestCase("ABC", "ABCD")]
        public void CharacterDuplicateListNameFails(string tListName, string tName)
        {
            var vDocumentStore = GetEmbeddedDatabase;

            AddAccountToDataStore(vDocumentStore, cUserName, "ABC", "test@email.com");

            var vModel = new CreateCharacterModel
            {
                GameName = cGameName,
                ListName = tListName,
                Name = tName,
            };

            var vCommand = new CreateCharacterCommand(vDocumentStore, vModel, cUserName);

            vCommand.Execute();

            Assert.AreEqual(eCharacterCreationStatus.DuplicateListName, vCommand.Execute());
        }
    }
}