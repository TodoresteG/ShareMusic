using ShareMusic.Models.Groups;

namespace ShareMusic.Services.Interfaces
{
    public interface IGroupsService
    {
        CreateGroupInputModel ListAllUsers();

        void CreateGroup(CreateGroupInputModel inputModel);

        GroupsListViewModel ListAllGroupsForUser(string userId);
    }
}
