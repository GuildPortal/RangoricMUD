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
using RangoricMUD.Dice;

#endregion

namespace RangoricMUD.Tests.Dice
{
    ///<summary>
    ///  This is a test class for CryptoRandomProviderTest and is intended to contain all CryptoRandomProviderTest Unit Tests
    ///</summary>
    [TestFixture]
    public class CryptoRandomProviderTest
    {
        private IRandomProvider mDice;

        [TestFixtureSetUp]
        public void Setup()
        {
            mDice = new CryptoRandomProvider();
        }

        [Test]
        public void RandomByte()
        {
            var vGood = false;
            for(var vIndex = 0;vIndex < 100;vIndex++)
            {
                vGood |= mDice.GetByte() != default(byte);
            }
            Assert.IsTrue(vGood);
        }

        [Test]
        public void RandomCharacter()
        {
            var vGood = false;
            for (var vIndex = 0; vIndex < 100; vIndex++)
            {
                vGood |= mDice.GetCharacter() != default(char);
            }
            Assert.IsTrue(vGood);
        }

        [TestCase(1, 6)]
        [TestCase(1, 1)]
        [TestCase(7, 200)]
        [TestCase(100000, 2000000000)]
        public void RandomIntegerInRange(int tMin, int tMax)
        {
            var vResult = mDice.GetInteger(tMin, tMax);
            Assert.True(vResult >= tMin);
            Assert.True(vResult <= tMax);
        }

        [Test]
        public void RandomString()
        {
            var vGood = false;
            for (var vIndex = 0; vIndex < 100; vIndex++)
            {
                vGood |= mDice.GetString(512) != default(string);
            }
            Assert.IsTrue(vGood);
        }
        [Test]
        public void RandomStringDoesntGiveEmptyString()
        {
            var vGood = false;
            for (var vIndex = 0; vIndex < 100; vIndex++)
            {
                vGood |= mDice.GetString(512) != string.Empty;
            }
            Assert.IsTrue(vGood);

        }
    }
}