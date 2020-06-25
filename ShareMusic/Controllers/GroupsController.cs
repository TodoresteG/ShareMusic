using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMusic.Models.Groups;
using ShareMusic.Services.Interfaces;
using ShareMusic.Extensions;

namespace ShareMusic.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly IGroupsService groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            this.groupsService = groupsService;
        }

        public IActionResult CreateGroup()
        {
            CreateGroupInputModel viewModel = new CreateGroupInputModel { MultiSelectUsers = this.groupsService.ListAllUsers() };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateGroup(CreateGroupInputModel inputModel) 
        {
            if (!ModelState.IsValid)
            {
                inputModel.MultiSelectUsers = this.groupsService.ListAllUsers();
                return this.View(inputModel);
            }

            inputModel.OwnerId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            this.groupsService.CreateGroup(inputModel);

            return this.Redirect("List");
        }

        public IActionResult List() 
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            GroupsListViewModel viewModel = this.groupsService.ListAllGroupsForUser(userId);
            return this.View(viewModel);
        }

        public IActionResult Search(string searchText)
        {
            GroupsSearchResultViewModel viewModel = this.groupsService.SearchGroups(searchText);
            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            if (id.IsNullOrEmptyOrWhiteSpace())
            {
                return this.RedirectToAction("List");
            }

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            GroupDetailsViewModel groupDetails = this.groupsService.GetGroupDetails(id, userId);
            return this.View(groupDetails);
        }

        [HttpPost]
        public IActionResult Details(UsersListViewComponentViewModel inputModel, string id)
        {
            if (!ModelState.IsValid)
            {
                string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                GroupDetailsViewModel groupDetails = this.groupsService.GetGroupDetails(id, userId);
                return this.View(groupDetails);
            }

            this.groupsService.AddUsers(inputModel, id);
            return this.RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult RemoveUser(string username, string groupId)
        {
            if (username.IsNullOrEmptyOrWhiteSpace() || groupId.IsNullOrEmptyOrWhiteSpace())
            {
                return this.RedirectToAction("List");
            }

            this.groupsService.RemoveUser(username, groupId);
            return this.RedirectToAction("Details", new { id = groupId });
        }

        [HttpPost]
        public IActionResult DeleteGroup(string groupName) 
        {
            if (groupName.IsNullOrEmptyOrWhiteSpace())
            {
                return RedirectToAction("List");
            }

            this.groupsService.DeleteGroup(groupName);

            return this.RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult AddSong(string selectedGroup, int songId) 
        {
            if (selectedGroup.IsNullOrEmptyOrWhiteSpace() || songId <= 0)
            {
                return this.Redirect("/");
            }

            string groupId = this.groupsService.AddSong(selectedGroup, songId);
            return this.RedirectToAction("Details", new { id = groupId });
        }

        [HttpPost]
        public IActionResult RemoveSong(int songId, string groupName) 
        {
            if (groupName.IsNullOrEmptyOrWhiteSpace() || songId <= 0)
            {
                return this.RedirectToAction("List");
            }

            string groupId = this.groupsService.RemoveSong(songId, groupName);
            if (string.IsNullOrEmpty(groupId))
            {
                return this.RedirectToAction("List");
            }

            return this.RedirectToAction("Details", new { id = groupId });
        }
    }
}
