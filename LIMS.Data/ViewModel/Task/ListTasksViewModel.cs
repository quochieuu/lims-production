namespace LIMS.Data.ViewModel.Task
{
    public class ListTasksViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int SortOrder { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsStarred { get; set; }
        public bool IsPriority { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsOutOfDate { get; set; }
        public bool IsArchived { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public List<LabelDto> labels { get; set; }
    }

    public class LabelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
