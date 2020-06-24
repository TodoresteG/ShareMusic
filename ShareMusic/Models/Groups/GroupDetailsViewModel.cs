using System.Collections.Generic;

namespace ShareMusic.Models.Groups
{
    public class GroupDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public ICollection<string> GroupUserNames { get; set; }

        public ICollection<GroupSongsViewModel> Songs { get; set; }

        public bool IsUserInGroup { get; set; }
    }
}
