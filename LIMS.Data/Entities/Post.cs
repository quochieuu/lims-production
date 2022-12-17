namespace LIMS.Data.Entities
{
    public class Post : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? PlaceId { get; set; }
        public DateTime? VisitedDate { get; set; }
    }
}
