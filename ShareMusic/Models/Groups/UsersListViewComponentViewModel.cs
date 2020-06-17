using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareMusic.Attributes;

namespace ShareMusic.Models.Groups
{
    public class UsersListViewComponentViewModel
    {
        [Display(Name = "Add users to group")]
        public MultiSelectList MultiSelectUsers { get; set; }

        [EnsureOneUserValidation]
        public IEnumerable<string> SelectedUsers { get; set; }
    }
}
