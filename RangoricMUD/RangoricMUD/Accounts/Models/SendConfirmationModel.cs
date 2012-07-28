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

namespace RangoricMUD.Accounts.Models
{
    public class SendConfirmationModel
    {
        public SendConfirmationModel(string tName, string tEmail, int tConfirmationNumber)
        {
            Name = tName;
            Email = tEmail;
            ConfirmationNumber = tConfirmationNumber;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public int ConfirmationNumber { get; private set; }
    }
}