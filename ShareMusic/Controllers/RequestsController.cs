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

        [HttpPost]
        public IActionResult Approve(string groupId, string requestId, string userName) 
        {
            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(requestId) || string.IsNullOrEmpty(userName))
            {
                return this.RedirectToAction("List", "Groups");
            }

            this.requestsService.ApproveRequest(groupId, requestId, userName);
            return this.RedirectToAction("Details", "Groups", new { id = groupId });
        }

        [HttpPost]
        public IActionResult Decline(string groupId, string requestId, string userName) 
        {
            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(requestId) || string.IsNullOrEmpty(userName))
            {
                return this.RedirectToAction("List", "Groups");
            }

            this.requestsService.DeclineRequest(groupId, requestId, userName);
            return this.RedirectToAction("All", new { groupId = groupId });
        }
    }
}
