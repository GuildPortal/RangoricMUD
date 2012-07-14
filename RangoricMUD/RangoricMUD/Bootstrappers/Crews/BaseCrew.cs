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

using System.Collections.Generic;

#endregion

namespace Rangoric.Website.Bootstrappers.Crews
{
    public abstract class BaseCrew : ICrew
    {
        private List<IBoot> mBoots;

        #region ICrew Members

        public void StrapOn(List<IBoot> tBoots)
        {
            mBoots = tBoots;
            StrapOn();
        }

        public void StrapOnCollections(List<IBoot> tBoots)
        {
        }

        #endregion

        protected IBoot Add()
        {
            var vBoot = new Boot();
            mBoots.Add(vBoot);
            return vBoot;
        }

        protected abstract void StrapOn();
    }
}