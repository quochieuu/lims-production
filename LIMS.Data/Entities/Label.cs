namespace LIMS.Data.Entities
{
    public class Label : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string? Slug { get; set; }
    }
}
