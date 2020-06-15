using System.ComponentModel.DataAnnotations;
using System.Linq;
using ShareMusic.Data;
using ShareMusic.Data.Entities;

namespace ShareMusic.Attributes
{
    public class GroupNameValidationAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => $"The name of the group is already used. Try another one.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string groupName = value?.ToString();
            ShareMusicDbContext context = (ShareMusicDbContext)validationContext.GetService(typeof(ShareMusicDbContext));

            bool doesNameExists = context
                    .Groups
                    .Any(g => g.Name == groupName);

            if (doesNameExists)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
