using System;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class SongAndArtistNamesSplitterService : ISongAndArtistNamesSplitterService
    {
        private readonly string[] featureTexts = new string[]
        {
            "/", "&", "ft", "ft.", "feat.", "x", ",", "X"
        };

        public string SplitArtistNames(string artistNames)
        {
            artistNames = artistNames.Trim(' ');

            string[] names = artistNames.Split(featureTexts, StringSplitOptions.RemoveEmptyEntries);

            return string.Empty;
        }
    }
}
