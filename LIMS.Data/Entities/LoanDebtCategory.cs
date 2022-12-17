namespace LIMS.Data.Entities
{
    public class LoanDebtCategory : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public int Type { get; set; } // 1. Loan // 2. Debt
    }
}
