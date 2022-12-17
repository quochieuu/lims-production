using LIMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIMS.Data.Configuration
{
    public class PostPlaceConfiguration : IEntityTypeConfiguration<PostPlace>
    {
        public void Configure(EntityTypeBuilder<PostPlace> builder)
        {
            builder.ToTable("PostPlaces");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.Slug).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(256);
        }
    }
}
