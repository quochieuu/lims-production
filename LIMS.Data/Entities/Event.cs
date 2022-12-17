namespace LIMS.Data.Entities
{
    public class Event : AuditableBaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Background { get; set; }
        public int Type { get; set; } // up, down
        public DateTime EventDate { get; set; }
    }
}
