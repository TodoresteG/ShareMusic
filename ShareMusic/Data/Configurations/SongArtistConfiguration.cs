using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class SongArtistConfiguration : IEntityTypeConfiguration<SongArtist>
    {
        public void Configure(EntityTypeBuilder<SongArtist> songArtist)
        {
            songArtist
                .HasKey(sa => new { sa.SongId, sa.ArtistId });
        }
    }
}
