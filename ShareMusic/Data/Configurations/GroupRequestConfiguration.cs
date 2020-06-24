using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class GroupRequestConfiguration : IEntityTypeConfiguration<GroupRequest>
    {
        public void Configure(EntityTypeBuilder<GroupRequest> groupRequest)
        {
            groupRequest
                .HasKey(gr => new { gr.GroupId, gr.RequestId });
        }
    }
}
