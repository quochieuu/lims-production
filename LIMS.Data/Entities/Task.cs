namespace LIMS.Data.Entities
{
    public class Task : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int SortOrder { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsStarred { get; set; }
        public bool IsPriority { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsOutOfDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
