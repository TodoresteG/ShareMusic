using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class SongArtist : BaseDeletableModel<int>
    {
        public int SongId { get; set; }

        public Song Song { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
