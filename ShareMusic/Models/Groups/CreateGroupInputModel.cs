using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShareMusic.Models.Groups
{
    public class CreateGroupInputModel
    {
        [Required]
        [StringLength(40, ErrorMessage = "{0} must be between {1} and {2} characters", MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<SelectListItem> Users { get; set; }
    }
}
