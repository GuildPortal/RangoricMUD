#region LIcense

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

#endregion

namespace RangoricMUD.Characters.Data
{
    public class Character
    {
        private string mID;

        public string Id
        {
            get
            {
                mID = mID ?? GenerateID();
                return mID;
            }
            set { mID = value; }
        }

        public string Name { get; set; }
        public string AccountName { get; set; }
        public string GameName { get; set; }

        private string GenerateID()
        {
            return GameName + "/Characters/" + AccountName + "/" + Name;
        }
    }
}