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

using System.Configuration;

#endregion

namespace Rangoric.Website.Configuration
{
    public class RangoricConfiguration : IRangoricConfiguration

    {
        #region IRangoricConfiguration Members

        public string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        public string PublicKey
        {
            get { return ConfigurationManager.AppSettings["PublicKey"]; }
        }

        public string PrivateKey
        {
            get { return ConfigurationManager.AppSettings["PrivateKey"]; }
        }

        #endregion
    }
}