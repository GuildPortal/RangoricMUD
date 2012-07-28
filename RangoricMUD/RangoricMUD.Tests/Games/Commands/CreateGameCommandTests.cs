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

using System.Linq;
using NUnit.Framework;
using RangoricMUD.Games.Commands;
using RangoricMUD.Games.Data;
using RangoricMUD.Games.Models;
using RangoricMUD.Tests.Utilities;

#endregion

namespace RangoricMUD.Tests.Games.Commands
{
    [TestFixture]
    public class CreateGameCommandTests : BaseTests
    {
        [TestCase("ABC")]
        public void CreatesGameInRaven(string tName)
        {
            var vDocumentStore = GetEmbeddedDatabase;

            var vModel = new CreateGameModel {Name = tName};

            var vCommand = new CreateGameCommand(vDocumentStore, vModel);
            vCommand.Execute();

            var vSession = vDocumentStore.OpenSession();
            var vObjects = vSession.Query<Game>().Where(t => t.Name == tName);

            Assert.AreEqual(1, vObjects.Count());
        }

        [TestCase("ABC")]
        public void DuplicateNamedGameIsCatchAndNotAllowed(string tName)
        {
            var vDocumentStore = GetEmbeddedDatabase;

            var vModel = new CreateGameModel {Name = tName};

            var vCommand = new CreateGameCommand(vDocumentStore, vModel);
            vCommand.Execute();
            Assert.AreEqual(eGameCreationStatus.DuplicateName, vCommand.Execute());
        }
    }
}