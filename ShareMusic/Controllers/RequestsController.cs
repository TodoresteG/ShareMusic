using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IRequestsService requestsService;

        public RequestsController(
            IRequestsService requestsService)
        {
            this.requestsService = requestsService;
        }

        [HttpPost]
        public IActionResult Join(string groupId, string userName) 
        {
            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(userName))
            {
                return this.Redirect("/");
            }

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            this.requestsService.Join(groupId, userName, userId);

            return View();
        }
    }
}
