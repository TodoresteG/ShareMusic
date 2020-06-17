using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareMusic.Attributes
{
    public class EnsureOneUserValidationAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => $"You need to select at least one user.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IList<string> list = value as IList<string>;

            if (list != null)
            {
                bool isEmpty = list.Count > 0;

                if (isEmpty)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
