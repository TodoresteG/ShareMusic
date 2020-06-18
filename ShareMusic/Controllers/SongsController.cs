using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public SongsController(
            ISongsService songsService,
            IYoutubeDataProvider youtubeDataProvider,
            ISongAndArtistNamesSplitterService splitterService)
        {
            this.songsService = songsService;
            this.youtubeDataProvider = youtubeDataProvider;
            this.splitterService = splitterService;
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
            ;
            // string videoId = this.youtubeDataProvider.SearchVideo(inputModel.Artist, inputModel.Song);
            // int songId = this.songsService.CreateSong(inputModel);

            return Redirect("Home");
        }
    }
}
