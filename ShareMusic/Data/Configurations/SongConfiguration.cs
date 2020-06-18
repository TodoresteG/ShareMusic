using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> song)
        {
            song
                .HasMany(s => s.Artists)
                .WithOne(a => a.Song)
                .HasForeignKey(a => a.SongId);

            song
                .HasMany(s => s.Metadata)
                .WithOne(m => m.Song)
                .HasForeignKey(m => m.SongId);

            song
                .HasMany(s => s.Playlists)
                .WithOne(p => p.Song)
                .HasForeignKey(p => p.SongId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
