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
using RangoricMUD.Games.Queries;
using RangoricMUD.Tests.Utilities;

#endregion

namespace RangoricMUD.Tests.Games.Queries
{
    [TestFixture]
    public class GetAllGamesQueryTests : BaseTests
    {
        [TestCase("ABC", "BCD")]
        public void GetAllGamesQueryGetsAllGames(string tGame1, string tGame2)
        {
            var vDatabase = GetEmbeddedDatabase;

            AddGameToDatabase(tGame1, vDatabase);
            AddGameToDatabase(tGame2, vDatabase);

            var vQuery = new GetAllGamesQuery(vDatabase);

            Assert.AreEqual(2, vQuery.Result.Count);
        }
    }
}