using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RangoricMUD.Games.Models
{
    public class CreateGameModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}