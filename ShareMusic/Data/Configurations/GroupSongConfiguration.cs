using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class GroupSongConfiguration : IEntityTypeConfiguration<GroupSong>
    {
        public void Configure(EntityTypeBuilder<GroupSong> groupSong)
        {
            groupSong
                .HasKey(gs => new { gs.GroupId, gs.SongId });
        }
    }
}
