using ShareMusic.DataProviders.Interfaces;
using Google.Apis.YouTube.v3;

namespace ShareMusic.DataProviders
{
    public class YoutubeDataProvider : IYoutubeDataProvider
    {
        public string SearchVideo(string artist, string song)
        {
            return "Not yet";
        }
    }
}
