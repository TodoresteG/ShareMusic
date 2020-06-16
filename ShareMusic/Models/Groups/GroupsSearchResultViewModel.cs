using System.Collections.Generic;

namespace ShareMusic.Models.Groups
{
    public class GroupsSearchResultViewModel
    {
        public int Count { get; set; }

        public ICollection<GroupsSearchResultListViewModel> SearchResults { get; set; }

        public string SearchText { get; set; }
    }
}
