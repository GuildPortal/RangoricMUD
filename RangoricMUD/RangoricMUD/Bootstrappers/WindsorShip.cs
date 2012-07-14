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
using RangoricMUD.Bootstrappers.Crews;

#endregion

namespace RangoricMUD.Bootstrappers
{
    public class WindsorShip : PirateShip
    {
        public WindsorShip()
            : base(new WindsorCaptain(),
                   new List<ICrew>
                       {
                           new WindsorCrew()
                       })
        {
            if (mCaptain is WindsorCaptain)
            {
                var vCaptain = mCaptain as WindsorCaptain;
                vCaptain.Register(this as IShip);
            }
        }
    }
}