using Microsoft.AspNetCore.Mvc.Rendering;
using ShareMusic.Models.Groups;

namespace ShareMusic.Services.Interfaces
{
    public interface IGroupsService
    {
        MultiSelectList ListAllUsers();

        void CreateGroup(CreateGroupInputModel inputModel);

        GroupsListViewModel ListAllGroupsForUser(string userId);

        GroupsSearchResultViewModel SearchGroups(string groupName);

        GroupDetailsViewModel GetGroupDetails(string groupId);

        void AddUsers(UsersListViewComponentViewModel inputModel, string groupId);

        void RemoveUser(string username, string groupName);

        void DeleteGroup(string groupName);
    }
}
