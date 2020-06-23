using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShareMusic.DataProviders.Interfaces;
using ShareMusic.Models.Songs;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongsService songsService;
        private readonly IYoutubeDataProvider youtubeDataProvider;
        private readonly ISongAndArtistNamesSplitterService splitterService;
        private readonly ISongMetadataService metadataService;
        private readonly IGeniusLyricsDataProvider geniusLyrics;

        private readonly ILogger<SongsController> logger;

        public SongsController(
            ISongsService songsService,
            IYoutubeDataProvider youtubeDataProvider,
            ISongAndArtistNamesSplitterService splitterService,
            ISongMetadataService metadataService,
            IGeniusLyricsDataProvider geniusLyrics,
            ILogger<SongsController> logger)
        {
            this.songsService = songsService;
            this.youtubeDataProvider = youtubeDataProvider;
            this.splitterService = splitterService;
            this.metadataService = metadataService;
            this.geniusLyrics = geniusLyrics;
            this.logger = logger;
        }

        [Authorize]
        public IActionResult AddSong()
        {
            AddSongInputModel inputModel = new AddSongInputModel();
            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddSong(AddSongInputModel inputModel) 
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            List<string> artists = this.splitterService.SplitArtistName(inputModel.Artist).ToList();
            int songId = this.songsService.CreateSong(inputModel.Song, artists);

            string videoId = this.youtubeDataProvider.SearchVideo(string.Join(" ", artists), inputModel.Song);
            if (!string.IsNullOrEmpty(videoId))
            {
                this.metadataService.AddMetadataInfo(songId, "YoutubeVideo", videoId);
            }
            else
            {
                this.logger.LogWarning($"Video for {string.Join(" ", artists)} {inputModel.Song} not found");
            }

            string lyrics = this.geniusLyrics.AskForLyrics(inputModel.Song, string.Join(" ", artists));
            if (!string.IsNullOrEmpty(lyrics))
            {
                this.metadataService.AddMetadataInfo(songId, "Lyrics", lyrics);
            }
            else
            {
                this.logger.LogWarning($"Lyrics for {string.Join(" ", artists)} {inputModel.Song} not found");
            }

            this.songsService.UpdateSongsSystemData(songId);

            return Redirect("/");
        }

        public IActionResult Details(int songId) 
        {
            if (songId <= 0)
            {
                return Redirect("/");
            }

            SongDetailsViewModel viewModel = this.songsService.GetDetails(songId);
            return this.View(viewModel);
        }
    }
}
