using System.Collections.Generic;
using ShareMusic.Models.Home;

namespace ShareMusic.Models.Songs
{
    public class SongsSearchResultViewModel
    {
        public ICollection<SongCardViewModel> SearchResults { get; set; }

        public string SearchText { get; set; }
    }
}
