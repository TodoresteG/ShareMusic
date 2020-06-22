using System.Collections.Generic;

namespace ShareMusic.Models.Home
{
    public class HomeRecentSongsViewModel
    {
        public ICollection<SongCardViewModel> NewestSongs { get; set; }
    }
}
