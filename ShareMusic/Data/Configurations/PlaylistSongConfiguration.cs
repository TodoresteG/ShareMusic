using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class PlaylistSongConfiguration : IEntityTypeConfiguration<PlaylistSong>
    {
        public void Configure(EntityTypeBuilder<PlaylistSong> playlistSong)
        {
            playlistSong
                .HasKey(ps => new { ps.PlaylistId, ps.SongId });
        }
    }
}
