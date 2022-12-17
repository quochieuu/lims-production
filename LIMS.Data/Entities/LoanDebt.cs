namespace LIMS.Data.Entities
{
    public class LoanDebt : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Note { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; } // 1. Loan // 2. Debt
        public DateTime Duration { get; set; }
    }
}
