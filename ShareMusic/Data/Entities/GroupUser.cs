using ShareMusic.Data.Entities.Common;

namespace ShareMusic.Data.Entities
{
    public class GroupUser : BaseModel<int>
    {
        public string GroupId { get; set; }

        public Group Group { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
