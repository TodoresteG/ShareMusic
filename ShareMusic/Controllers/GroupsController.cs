using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMusic.Models.Groups;
using ShareMusic.Services.Interfaces;

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
            CreateGroupInputModel viewModel = this.groupsService.ListAllUsers();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateGroup(CreateGroupInputModel inputModel) 
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }


            return Redirect("/");
        }
    }
}
