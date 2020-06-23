using System.Collections.Generic;

namespace ShareMusic.Models.Groups
{
    public class GroupDetailsViewModel
    {
        public string Name { get; set; }

        public string OwnerName { get; set; }

        public ICollection<string> GroupUserNames { get; set; }

        public ICollection<GroupSongsViewModel> Songs { get; set; }
    }
}
