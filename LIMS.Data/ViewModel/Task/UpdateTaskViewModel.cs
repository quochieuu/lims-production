namespace LIMS.Data.ViewModel.Task
{
    public class UpdateTaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int SortOrder { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsStarred { get; set; }
        public bool IsPriority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
