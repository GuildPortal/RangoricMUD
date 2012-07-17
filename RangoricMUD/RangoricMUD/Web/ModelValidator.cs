using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RangoricMUD.Web
{
    public static class ModelValidator
    {
        /// <summary>
        /// Validates the model. Doesn't return the errors for when we don't care.
        /// </summary>
        /// <param name="tObject"></param>
        /// <returns></returns>
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