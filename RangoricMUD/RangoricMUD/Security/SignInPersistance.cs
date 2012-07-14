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
using System.Linq;
using System.Web;
using System.Web.Security;

#endregion

namespace Rangoric.Website.Security
{
    public class SignInPersistance : ISignInPersistance
    {
        private const string cCookieName = "Website Authentication";

        #region ISignInPersistance Members

        public string AccountName
        {
            get
            {
                string vReturn = null;
                HttpCookie vCookie = null;
                if (HttpContext.Current.Request.Cookies.AllKeys.Any(t => t == cCookieName))
                {
                    vCookie = HttpContext.Current.Request.Cookies[cCookieName];
                }
                if (vCookie != null)
                {
                    vReturn = FormsAuthentication.Decrypt(vCookie.Value).Name;
                }
                return vReturn;
            }
            set
            {
                var vCookie = FormsAuthentication.GetAuthCookie(value, true);
                vCookie.Name = cCookieName;
                HttpContext.Current.Response.Cookies.Add(vCookie);
            }
        }

        public void SignOut()
        {
            var vCookie = HttpContext.Current.Request.Cookies[cCookieName];
            vCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(vCookie);
        }

        #endregion
    }
}