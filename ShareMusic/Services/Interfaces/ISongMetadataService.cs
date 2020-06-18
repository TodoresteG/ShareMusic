namespace ShareMusic.Services.Interfaces
{
    public interface ISongMetadataService
    {
        void AddMetadataInfo(int songId, string type, string value);
    }
}
