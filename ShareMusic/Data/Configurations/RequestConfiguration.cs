using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> request)
        {
            request
                .HasMany(r => r.GroupRequests)
                .WithOne(gr => gr.Request)
                .HasForeignKey(gr => gr.RequestId);
        }
    }
}
