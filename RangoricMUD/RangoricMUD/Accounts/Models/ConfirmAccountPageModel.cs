using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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