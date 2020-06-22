namespace ShareMusic.Models.GeniusLyricsDataProvider.Search
{
    public class SearchLyricsModel
    {
        public Meta meta { get; set; }

        public Response response { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
    }

    public class Response
    {
        public Hit[] hits { get; set; }
    }

    public class Hit
    {
        public string type { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public string api_path { get; set; }

        public string lyrics_state { get; set; }

        public string title { get; set; }
    }
}
