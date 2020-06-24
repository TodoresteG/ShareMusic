using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMusic.Models.Requests;
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

        public IActionResult All(string groupId) 
        {
            if (string.IsNullOrEmpty(groupId) || string.IsNullOrWhiteSpace(groupId))
            {
                return this.Redirect("/");
            }

            AllGroupRequestsViewModel viewModel = this.requestsService.ListAllReuqestsForGroup(groupId);
            return this.View(viewModel);
        }
    }
}
