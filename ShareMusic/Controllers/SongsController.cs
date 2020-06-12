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

        public SongsController(ISongsService songsService, IYoutubeDataProvider youtubeDataProvider)
        {
            this.songsService = songsService;
            this.youtubeDataProvider = youtubeDataProvider;
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

            // this.songsService.CreateSong(inputModel);
            this.youtubeDataProvider.SearchVideo(inputModel.Artist, inputModel.Song);

            return Redirect("Home");
        }
    }
}
