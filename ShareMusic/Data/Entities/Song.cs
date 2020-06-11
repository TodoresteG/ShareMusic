using System.Collections.Generic;
using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class Song : BaseDeletableModel<int>
    {
        public Song()
        {
            this.Artists = new HashSet<SongArtist>();
            this.Playlists = new HashSet<PlaylistSong>();
        }

        public string Name { get; set; }

        public ICollection<SongArtist> Artists { get; set; }

        public ICollection<PlaylistSong> Playlists { get; set; }
    }
}
