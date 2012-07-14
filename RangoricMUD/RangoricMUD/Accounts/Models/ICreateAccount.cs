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
using DataAnnotationsExtensions;

#endregion

namespace Rangoric.Website.Accounts.Models
{
    public interface ICreateAccount
    {
        [Required]
        [Display(Name = "Account Name")]
        [StringLength(100, MinimumLength = 3)]
        string Name { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [Email]
        [StringLength(100, MinimumLength = 5)]
        string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 10)]
        string Password { get; set; }
    }
}