namespace LIMS.Data.Entities
{
    public class PostCommentImage : AuditableBaseEntity
    {
        public Guid CommentId { get; set; }
        public string Path { get; set; }
    }
}
