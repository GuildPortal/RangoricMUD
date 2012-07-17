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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

#endregion

namespace RangoricMUD.Security
{
    public class SignInPersistance : ISignInPersistance
    {
        private static Dictionary<string, string> Logins = new Dictionary<string, string>();
        #region ISignInPersistance Members

        public string AccountName(string tConnectionID)
        {
            if (Logins.ContainsKey(tConnectionID))
            {
                return Logins[tConnectionID];
            }
            else
            {
                return null;
            }

        }

        public void Login(string tName, string tConnectionID)
        {
            if(Logins.ContainsKey(tConnectionID))
            {
                Logins.Remove(tConnectionID);
            }
            Logins.Add(tConnectionID, tName);
        }

        public void Logout(string tAccountName)
        {
            var vIDs = Logins.Where(t => t.Value == tAccountName).Select(t => t.Key);

            foreach(var vID in vIDs)
            {
                Logins.Remove(vID);
            }
        }

        #endregion
    }
}