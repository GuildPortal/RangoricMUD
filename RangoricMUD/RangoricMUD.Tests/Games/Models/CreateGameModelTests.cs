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
using RangoricMUD.Web;

#endregion

namespace RangoricMUD.Tests.Games.Models
{
    [TestFixture]
    public class CreateGameModelTests
    {
        [TestCase("ABC")]
        [TestCase("ABCD")]
        public void GameIsValid(string tName)
        {
            var vCreateGameModel = new CreateGameModel
                                       {
                                           Name = tName,
                                       };

            Assert.IsTrue(ModelValidator.IsValid(vCreateGameModel));
        }

        [TestCase("")]
        [TestCase("A")]
        [TestCase("AB")]
        public void GameIsInvalid(string tName)
        {
            var vCreateGameModel = new CreateGameModel
                                       {
                                           Name = tName,
                                       };

            Assert.IsFalse(ModelValidator.IsValid(vCreateGameModel));
        }
    }
}