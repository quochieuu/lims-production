namespace LIMS.Data.Entities
{
    public class AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
