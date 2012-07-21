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

using NUnit.Framework;
using RangoricMUD.Games.Models;
using RangoricMUD.Games.Queries;
using RangoricMUD.Tests.Utilities;

#endregion

namespace RangoricMUD.Tests.Games.Queries
{
    [TestFixture]
    public class GetGameQueryTests : BaseTests
    {
        [TestCase("ABC", new[] {"ABC", "BCD", "CDE"})]
        [TestCase("BCD", new[] {"ABC", "BCD", "CDE"})]
        [TestCase("CDE", new[] {"ABC", "BCD", "CDE"})]
        public void GetGameFromDocumentStore(string tGameName, string[] tGameList)
        {
            var vDatabase = GetEmbeddedDatabase;

            foreach (var vGameName in tGameList)
            {
                AddGameToDatabase(vGameName, vDatabase);
            }

            var vModel = new GetGameModel {Name = tGameName};

            var vQuery = new GetGameQuery(vDatabase, vModel);

            Assert.AreEqual(tGameName, vQuery.Result.Name);
        }
    }
}