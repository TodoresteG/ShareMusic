using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> playlist)
        {
            playlist
                .HasMany(p => p.Songs)
                .WithOne(s => s.Playlist)
                .HasForeignKey(s => s.PlaylistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
