namespace ShareMusic.Data.Entities
{
    public class GroupRequest
    {
        public string GroupId { get; set; }

        public Group Group { get; set; }

        public string RequestId { get; set; }

        public Request Request { get; set; }
    }
}
