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

using System.Collections.Generic;

#endregion

namespace RangoricMUD.Accounts.Data
{
    public class Account
    {
        private List<string> mCharacters;

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

        /// <summary>
        /// The unique name of this account.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The email for this account.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The hashed password.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Has the user confirmed their account?
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// The roles this user belongs to.
        /// </summary>
        public List<eRoles> Roles { get; set; }

        public int ConfirmationNumber { get; set; }

        /// <summary>
        /// The Character IDs for this account's characters.
        /// </summary>
        public List<string> Characters
        {
            get
            {
                mCharacters = mCharacters ?? new List<string>();
                return mCharacters;
            }
            set { mCharacters = value; }
        }

        private string GenerateID()
        {
            return "Accounts/" + Name;
        }
    }
}