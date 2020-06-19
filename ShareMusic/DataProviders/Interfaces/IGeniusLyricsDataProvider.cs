namespace ShareMusic.DataProviders.Interfaces
{
    public interface IGeniusLyricsDataProvider
    {
        void GetLyrics(string songTitle, string artist);
    }
}
