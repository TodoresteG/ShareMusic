using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShareMusic.Controllers
{
    public class SongsController : Controller
    {
        [Authorize]
        public IActionResult AddSong() 
        {
            return this.View();
        }
    }
}
