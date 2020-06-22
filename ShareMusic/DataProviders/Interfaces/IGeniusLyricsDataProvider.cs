namespace ShareMusic.DataProviders.Interfaces
{
    public interface IGeniusLyricsDataProvider
    {
        string AskForLyrics(string songTitle, string artist);
    }
}
