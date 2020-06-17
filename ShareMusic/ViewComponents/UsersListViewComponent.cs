using Microsoft.AspNetCore.Mvc;
using ShareMusic.Models.Groups;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.ViewComponents
{
    [ViewComponent(Name = "UsersList")]
    public class UsersListViewComponent : ViewComponent
    {
        private readonly IGroupsService groupsService;

        public UsersListViewComponent(IGroupsService groupsService)
        {
            this.groupsService = groupsService;
        }

        public IViewComponentResult Invoke()
        {
            UsersListViewComponentViewModel viewModel = new UsersListViewComponentViewModel
            {
                MultiSelectUsers = this.groupsService.ListAllUsers()
            };

            return this.View(viewModel);
        }
    }
}
