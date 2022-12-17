namespace LIMS.Data.Entities
{
    public class PostImage : AuditableBaseEntity
    {
        public Guid PostId { get; set; }
        public string Path { get; set; }
    }
}
