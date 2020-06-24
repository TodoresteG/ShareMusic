using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> group)
        {
            group
                .HasMany(g => g.Songs)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId);

            group
                .HasMany(g => g.Users)
                .WithOne(u => u.Group)
                .HasForeignKey(u => u.GroupId);

            group
                .HasMany(g => g.Requests)
                .WithOne(gr => gr.Group)
                .HasForeignKey(gr => gr.GroupId);
        }
    }
}
