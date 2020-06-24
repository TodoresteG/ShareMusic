using System;
using System.Collections.Generic;
using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class Request : BaseModel<string>
    {
        public Request()
        {
            this.Id = Guid.NewGuid().ToString();
            this.GroupRequests = new HashSet<GroupRequest>();
        }

        public string Name { get; set; }

        public ICollection<GroupRequest> GroupRequests { get; set; }
    }
}
