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

using System;
using System.IO;
using System.Web;
using NUnit.Framework;
using Rangoric.Website.Security;

#endregion

namespace Rangoric.UnitTests.Security
{
    [TestFixture]
    public class SignInPersistanceTests
    {
        [Test]
        public void SignOutTest()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://www.rangoric.com", ""),
                new HttpResponse(new StringWriter()));

            var vPersistance = new SignInPersistance
                                   {
                                       AccountName = "Test"
                                   };

            HttpContext.Current.Request.Cookies.Clear();
            HttpContext.Current.Request.Cookies.Add(HttpContext.Current.Response.Cookies[0]);

            vPersistance.SignOut();
            Assert.IsTrue(DateTime.Now > HttpContext.Current.Response.Cookies[0].Expires);

            HttpContext.Current.Request.Cookies.Remove(HttpContext.Current.Response.Cookies[0].Name);

            Assert.AreEqual(null, vPersistance.AccountName);
        }

        [Test]
        public void UserPropertyWorksWhenItCan()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://www.rangoric.com", ""),
                new HttpResponse(new StringWriter()));

            var vPersistance = new SignInPersistance
                                   {
                                       AccountName = "Test"
                                   };

            HttpContext.Current.Request.Cookies.Clear();
            HttpContext.Current.Request.Cookies.Add(HttpContext.Current.Response.Cookies[0]);

            Assert.AreEqual("Test", vPersistance.AccountName);
        }
    }
}