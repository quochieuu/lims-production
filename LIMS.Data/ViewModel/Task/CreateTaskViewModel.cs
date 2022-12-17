namespace LIMS.Data.ViewModel.Task
{
    public class CreateTaskViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Labels { get; set; }
        public int SortOrder { get; set; }
        public DateTime Deadline { get; set; }
    }
}
