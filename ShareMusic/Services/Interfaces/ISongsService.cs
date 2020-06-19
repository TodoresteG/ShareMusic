using System.Collections.Generic;
using ShareMusic.Models.Songs;

namespace ShareMusic.Services.Interfaces
{
    public interface ISongsService
    {
        int CreateSong(string songTitle, IList<string> artists);

        void UpdateSongsSystemData(int songId);
    }
}
