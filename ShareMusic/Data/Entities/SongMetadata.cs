using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class SongMetadata : BaseModel<int>
    {
        public int SongId { get; set; }

        public Song Song { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}
