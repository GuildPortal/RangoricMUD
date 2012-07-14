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
            var vResultOne = mDice.GetByte();
            var vResultTwo = mDice.GetByte();
            Assert.AreNotEqual(vResultTwo, vResultOne);
        }

        [Test]
        public void RandomCharacter()
        {
            var vResultOne = mDice.GetCharacter();
            var vResultTwo = mDice.GetCharacter();
            Assert.AreNotEqual(vResultOne, vResultTwo);
        }

        [Test]
        public void RandomInteger()

        {
            var vResultOne = mDice.GetInteger(1, 10000);
            var vResultTwo = mDice.GetInteger(1, 10000);
            Assert.AreNotEqual(vResultOne, vResultTwo);
        }

        [Test]
        public void RandomIntegerInRange()
        {
            var vResult = mDice.GetInteger();
            Assert.True(vResult >= 1);
            Assert.True(vResult <= 6);
        }

        [Test]
        public void RandomString()
        {
            var vResultOne = mDice.GetString(512);
            var vResultTwo = mDice.GetString(512);
            Assert.AreNotEqual(vResultOne, vResultTwo);
        }
    }
}