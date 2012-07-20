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
using RangoricMUD.Security;

#endregion

namespace RangoricMUD.Tests.Security
{
    [TestFixture]
    public class CustomHashProviderTests
    {
        [TestCase("A")]
        [TestCase("AB")]
        public void HashingReturnsSomethingNew(string tString)
        {
            var vProvider = new CustomHashProvider();
            Assert.AreNotEqual(tString, vProvider.Hash(tString));
        }

        [TestCase("A")]
        [TestCase("AB")]
        public void HashingReturnsTheSameThingForTheSameInputs(string tString)
        {
            var vProvider = new CustomHashProvider();
            var vResult = vProvider.Hash(tString);
            Assert.IsTrue(vProvider.CheckHash(tString, vResult));
        }
    }
}