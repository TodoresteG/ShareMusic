using ShareMusic.Models.Songs;

namespace ShareMusic.Services.Interfaces
{
    public interface ISongsService
    {
        void CreateSong(AddSongInputModel inputModel);
    }
}
