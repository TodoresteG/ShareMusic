using ShareMusic.Models.Songs;

namespace ShareMusic.Services.Interfaces
{
    public interface ISongsService
    {
        int CreateSong(AddSongInputModel inputModel);
    }
}
