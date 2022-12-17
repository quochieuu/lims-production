using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LIMS.Data.Entities;

namespace LIMS.Data.Configuration
{
    public class LoanDebtCategoryConfiguration : IEntityTypeConfiguration<LoanDebtCategory>
    {
        public void Configure(EntityTypeBuilder<LoanDebtCategory> builder)
        {
            builder.ToTable("LoanDebtCategories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
}
