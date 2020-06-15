using System.Collections.Generic;
using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class Group : BaseDeletableModel<string>
    {
        public Group()
        {
            this.Songs = new HashSet<GroupSong>();
            this.Users = new HashSet<GroupUser>();
        }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<GroupSong> Songs { get; set; }

        public ICollection<GroupUser> Users { get; set; }
    }
}
