namespace LIMS.Data.ViewModel.Post
{
    public class UpdatePostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? PlaceId { get; set; }
        public DateTime? VisitedDate { get; set; }
    }
}
