using System.Collections.Generic;
using ShareMusic.Models.Home;
using ShareMusic.Models.Songs;

namespace ShareMusic.Services.Interfaces
{
    public interface ISongsService
    {
        int CreateSong(string songTitle, IList<string> artists);

        void UpdateSongsSystemData(int songId);

        HomeRecentSongsViewModel GetRecentSongs();

        SongDetailsViewModel GetDetails(int songId);
    }
}
