namespace LIMS.Data.Entities
{
    public class PostPlace : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}
