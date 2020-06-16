using System.Collections.Generic;

namespace ShareMusic.Models.Groups
{
    public class GroupsListViewModel
    {
        public ICollection<GroupUsersListViewModel> GroupsForUser { get; set; }

        public ICollection<GroupsOwnedByUserViewModel> OwnedGroups { get; set; }
    }
}
