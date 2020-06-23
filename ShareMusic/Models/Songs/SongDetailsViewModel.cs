using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShareMusic.Models.Songs
{
    public class SongDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string VideoId { get; set; }

        public string EmbededLyrics { get; set; }

        public MultiSelectList UserGroups { get; set; }

        public string SelectedGroup { get; set; }
    }
}
