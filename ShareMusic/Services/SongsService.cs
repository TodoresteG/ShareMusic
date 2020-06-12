using ShareMusic.Data;
using ShareMusic.Models.Songs;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class SongsService : ISongsService
    {
        private readonly ShareMusicDbContext context;

        public SongsService(ShareMusicDbContext context)
        {
            this.context = context;
        }

        public void CreateSong(AddSongInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
