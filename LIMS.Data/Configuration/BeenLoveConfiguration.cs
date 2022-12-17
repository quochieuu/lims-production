using LIMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIMS.Data.Configuration
{
    public class BeenLoveConfiguration : IEntityTypeConfiguration<BeenLove>
    {
        public void Configure(EntityTypeBuilder<BeenLove> builder)
        {
            builder.ToTable("BeenLoves");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
}
