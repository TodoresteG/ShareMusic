using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class PlaylistSong : BaseModel<int>
    {
        public int PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public int SongId { get; set; }

        public Song Song { get; set; }
    }
}
