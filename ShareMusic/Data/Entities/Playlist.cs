using System.Collections.Generic;
using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class Playlist : BaseModel<int>
    {
        public Playlist()
        {
            this.Songs = new HashSet<PlaylistSong>();
        }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<PlaylistSong> Songs { get; set; }
    }
}
