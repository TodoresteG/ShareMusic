using ShareMusic.Models.Requests;

namespace ShareMusic.Services.Interfaces
{
    public interface IRequestsService
    {
        void Join(string groupId, string userName, string userId);

        AllGroupRequestsViewModel ListAllReuqestsForGroup(string groupId);

        void ApproveRequest(string groupId, string requestId, string userName);

        void DeclineRequest(string groupId, string requestId, string userName);
    }
}
