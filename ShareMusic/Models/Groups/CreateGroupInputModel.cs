﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareMusic.Attributes;

namespace ShareMusic.Models.Groups
{
    public class CreateGroupInputModel
    {
        [Required]
        [GroupNameValidation]
        [StringLength(40, ErrorMessage = "{0} must be between {1} and {2} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Add users to group")]
        public MultiSelectList MultiSelectUsers { get; set; }

        [EnsureOneUserValidation]
        public IEnumerable<string> SelectedUsers { get; set; }

        public string OwnerId { get; set; }
    }
}
