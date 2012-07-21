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
using System.ComponentModel.DataAnnotations;

#endregion

namespace RangoricMUD.Web
{
    public static class ModelValidator
    {
        /// <summary>
        ///   Validates the model. Doesn't return the errors for when we don't care.
        /// </summary>
        /// <param name="tObject"> </param>
        /// <returns> </returns>
        public static bool IsValid(object tObject)
        {
            var vContext = new ValidationContext(tObject, null, null);
            var vResults = new List<ValidationResult>();
            return Validator.TryValidateObject(
                tObject, vContext, vResults, true
                );
        }
    }
}