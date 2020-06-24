namespace ShareMusic.Services.Interfaces
{
    public interface IRequestsService
    {
        void Join(string groupId, string userName, string userId);
    }
}
