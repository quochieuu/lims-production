using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIMS.Data.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<LIMS.Data.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<LIMS.Data.Entities.Task> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(x => x.Id);
        }
    }
}
