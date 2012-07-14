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

using System.ComponentModel.DataAnnotations;

namespace RangoricMUD.Accounts.Models
{
    public class LoginAccount
    {
        #region ILoginAccount Members

        [Required]
        [Display(Name = "Account Name", Prompt = "Account Name")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 10)]
        public string Password { get; set; }

        #endregion
    }
}