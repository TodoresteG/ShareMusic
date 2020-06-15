using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class GroupSong : BaseModel<int>
    {
        public string GroupId { get; set; }

        public Group Group { get; set; }

        public int SongId { get; set; }

        public Song Song { get; set; }
    }
}
