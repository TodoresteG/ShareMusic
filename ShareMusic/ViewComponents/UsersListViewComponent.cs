using Microsoft.AspNetCore.Mvc;
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
            // TODO: Use different viewModel
            var viewModel = this.groupsService.ListAllUsers();
            return this.View(viewModel);
        }
    }
}
