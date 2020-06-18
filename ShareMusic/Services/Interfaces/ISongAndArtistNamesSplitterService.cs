using System.Collections.Generic;

namespace ShareMusic.Services.Interfaces
{
    public interface ISongAndArtistNamesSplitterService
    {
        IList<string> SplitArtistName(string artistNames);
    }
}
