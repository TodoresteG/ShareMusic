using System.Collections.Generic;

namespace ShareMusic.Models.Requests
{
    public class AllGroupRequestsViewModel
    {
        public string GroupName { get; set; }

        public string GroupId { get; set; }

        public IEnumerable<GroupRequestViewModel> Requests { get; set; }
    }
}
