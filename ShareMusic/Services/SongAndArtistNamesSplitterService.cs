using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ShareMusic.Extensions;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class SongAndArtistNamesSplitterService : ISongAndArtistNamesSplitterService
    {
        private static readonly string[] ArtistsDelimiters =
        {
            " featuring ", " ft ", " ft. ", " ft.", " f. ", "(ft ", "(ft. ", "(ft.",
            "(f. ", "(f.", " feat ", " feat. ", "feat.", "(feat ", "(feat. ", "(feat.",
            " vs ", " vs. ", "vs.", " with ", "(with ", " and ", " и ", ";", " / ", "&", ","
        };

        private readonly string[] exceptions =
        {
            "Play and Win", "Play & Win", "Mark F. Angelo"
        };

        private readonly string pattern;

        public SongAndArtistNamesSplitterService()
        {
            this.pattern = "(" + string.Join("|", ArtistsDelimiters.Select(Regex.Escape).ToArray()) + ")";
        }

        public IList<string> SplitArtistName(string inputString)
        {
            HashSet<string> listOfArtistNames = new HashSet<string>();
            if (string.IsNullOrWhiteSpace(inputString))
            {
                return listOfArtistNames.ToList();
            }

            foreach (var exception in this.exceptions)
            {
                if (inputString.ToLower().Contains(exception.ToLower()))
                {
                    inputString = inputString.ReplaceCaseInsensitive(exception, string.Empty);
                    listOfArtistNames.Add(exception.Trim());
                }
            }

            string[] inputStringParts = Regex
                .Split(inputString, this.pattern, RegexOptions.IgnoreCase)
                .Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            foreach (var part in inputStringParts)
            {
                if (ArtistsDelimiters.Contains(part.ToLower()))
                {
                    continue;
                }

                string artistName = part.Trim();

                if (artistName.IndexOf(')') != -1 && artistName.IndexOf('(') == -1)
                {
                    artistName = artistName.Replace(")", string.Empty);
                }

                listOfArtistNames.Add(artistName.Trim());
            }


            return listOfArtistNames.ToList();
        }
    }
}
