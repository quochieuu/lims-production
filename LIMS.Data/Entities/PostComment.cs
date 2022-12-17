namespace LIMS.Data.Entities
{
    public class PostComment : AuditableBaseEntity
    {
        public Guid PostId { get; set; }
        public Guid ParentId { get; set; }
        public string Content { get; set; }
    }
}
