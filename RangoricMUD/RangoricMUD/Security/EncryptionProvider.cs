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

using System.Security.Cryptography;
using System.Text;
using Rangoric.Website.Configuration;

#endregion

namespace Rangoric.Website.Security
{
    public class EncryptionProvider : IEncryptionProvider
    {
        private readonly IRangoricConfiguration mRangoricConfiguration;

        public EncryptionProvider(IRangoricConfiguration tRangoricConfiguration)
        {
            mRangoricConfiguration = tRangoricConfiguration;
        }

        private RSACryptoServiceProvider Encryptor
        {
            get
            {
                var vResult = new RSACryptoServiceProvider(2024);

                //If we already have a key use that instead of the just randomly generated one.
                var vKey = mRangoricConfiguration.PrivateKey ?? mRangoricConfiguration.PublicKey;
                if (vKey != null)
                {
                    vResult.FromXmlString(vKey);
                }
                return vResult;
            }
        }

        #region IEncryptionProvider Members

        public byte[] Encrypt(string tStringToEncrypt)
        {
            var vBase = Encoding.UTF8.GetBytes(tStringToEncrypt);
            byte[] vResult;
            using (var vEncryptor = Encryptor)
            {
                vResult = vEncryptor.Encrypt(vBase, false);
            }

            return vResult;
        }

        public string Decrypt(byte[] tEncryptedString)
        {
            byte[] vUnencrypted;
            using (var vEncryptor = Encryptor)
            {
                vUnencrypted = vEncryptor.Decrypt(tEncryptedString, false);
            }
            return Encoding.UTF8.GetString(vUnencrypted);
        }

        #endregion
    }
}