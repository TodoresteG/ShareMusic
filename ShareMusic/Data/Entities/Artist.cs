using System.Collections.Generic;
using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class Artist : BaseDeletableModel<int>
    {
        public Artist()
        {
            this.Songs = new HashSet<SongArtist>();
        }

        public string Name { get; set; }

        public ICollection<SongArtist> Songs { get; set; }
    }
}
