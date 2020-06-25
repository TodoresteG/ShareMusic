using System;
using ShareMusic.Common;
using ShareMusic.Data;
using ShareMusic.Data.Entities;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class SongMetadataService : ISongMetadataService
    {
        private readonly ShareMusicDbContext context;

        public SongMetadataService(ShareMusicDbContext context)
        {
            this.context = context;
        }

        public void AddMetadataInfo(int songId, string type, string value)
        {
            if (type == GlobalConstants.Lyrics || type == GlobalConstants.YouTubeVideo)
            {
                SongMetadata songMetadata = new SongMetadata
                {
                    SongId = songId,
                    Type = type,
                    Value = value,
                    CreatedOn = DateTime.UtcNow,
                };

                this.context.SongMetadata.Add(songMetadata);
                this.context.SaveChanges();
            }
        }
    }
}
