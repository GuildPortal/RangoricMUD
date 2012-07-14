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
        [Test]
        public void HashingReturnsSomethingNew()
        {
            var vProvider = new CustomHashProvider();
            const string cString = "Test";
            Assert.AreNotEqual(cString, vProvider.Hash(cString));
        }

        [Test]
        public void HashingReturnsTheSameThingForTheSameInputs()
        {
            var vProvider = new CustomHashProvider();
            const string cString = "Test";
            var vResult = vProvider.Hash(cString);
            Assert.IsTrue(vProvider.CheckHash(cString, vResult));
        }
    }
}