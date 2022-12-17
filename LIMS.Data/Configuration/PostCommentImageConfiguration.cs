using LIMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIMS.Data.Configuration
{
    public class PostCommentImageConfiguration : IEntityTypeConfiguration<PostCommentImage>
    {
        public void Configure(EntityTypeBuilder<PostCommentImage> builder)
        {
            builder.ToTable("PostCommentImages");
            builder.HasKey(x => x.Id);
        }
    }
}
