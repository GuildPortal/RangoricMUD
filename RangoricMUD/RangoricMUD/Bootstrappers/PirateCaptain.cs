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

namespace RangoricMUD.Bootstrappers
{
    public abstract class PirateCaptain
    {
        public void InspectCrew(IEnumerable<ICrew> tCrew)
        {
            var vBoots = new List<IBoot>();
            var vBootCollections = new List<IBoot>();

            foreach (var vCrew in tCrew)
            {
                vCrew.StrapOn(vBoots);
                vCrew.StrapOnCollections(vBootCollections);
            }

            InspectBoots(vBoots);
            InspectBootCollections(vBootCollections);
        }

        protected abstract void InspectBoots(List<IBoot> tBoots);
        protected abstract void InspectBootCollections(List<IBoot> tBoots);
    }
}