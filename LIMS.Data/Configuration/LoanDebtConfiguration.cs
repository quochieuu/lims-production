using LIMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIMS.Data.Configuration
{
    public class LoanDebtConfiguration : IEntityTypeConfiguration<LoanDebt>
    {
        public void Configure(EntityTypeBuilder<LoanDebt> builder)
        {
            builder.ToTable("LoanDebts");
            builder.HasKey(x => x.Id);
        }
    }
}
