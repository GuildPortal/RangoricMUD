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

using System.ComponentModel.DataAnnotations;

#endregion

namespace RangoricMUD.Accounts.Models
{
    public class ConfirmAccountPageModel : LoginAccount
    {
        [Required]
        [Range(100000, 2000000000)]
        [Display(Name = "Confirmation Number", Prompt = "Confirmation Number")]
        public int ConfirmationNumber { get; set; }
    }
}