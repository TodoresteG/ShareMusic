namespace ShareMusic.Models.GeniusLyricsDataProvider.Get
{
    public class GetLyricsModel
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
        public Song song { get; set; }
    }

    public class Song
    {
        public string embed_content { get; set; }
        public string lyrics_state { get; set; }
    }
}
