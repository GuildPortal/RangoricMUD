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
using System.Security.Cryptography;

#endregion

namespace Rangoric.Website.Dice
{
    public class CryptoRandomProvider : IRandomProvider
    {
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        #region IRandomProvider Members

        public byte GetByte()
        {
            var vResult = new byte[1];
            Provider.GetBytes(vResult);
            return vResult.Single();
        }

        public int GetInteger(int tMin = 1, int tMax = 6)
        {
            var vRange = tMax - tMin + 1;
            var vMaxValue = Int32.MaxValue/vRange*vRange;
            var vResult = GetIntInRange(vMaxValue);
            return vResult%vRange + tMin;
        }

        public string GetString(int tLength)
        {
            var vRandomBytes = new byte[tLength*2];
            Provider.GetBytes(vRandomBytes);
            return ConvertBytesToString(vRandomBytes, 0, tLength);
        }

        public char GetCharacter()
        {
            var vResults = new byte[2];
            Provider.GetBytes(vResults);
            return BitConverter.ToChar(vResults, 0);
        }

        #endregion

        private static int GetIntInRange(int tMaxValue)
        {
            var vRandomBytes = new byte[4];
            Provider.GetBytes(vRandomBytes);
            var vResult = BitConverter.ToInt32(vRandomBytes, 0);
            return vResult >= tMaxValue || vResult < 0
                       ? GetIntInRange(tMaxValue)
                       : vResult;
        }

        private static string ConvertBytesToString(byte[] tBytes, int tIndex, int tLength)
        {
            return tIndex < tLength*2
                       ? BitConverter.ToChar(tBytes, tIndex) +
                         ConvertBytesToString(tBytes, tIndex + 2, tLength)
                       : "";
        }
    }
}