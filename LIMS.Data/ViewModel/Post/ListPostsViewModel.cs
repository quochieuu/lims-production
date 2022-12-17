namespace LIMS.Data.ViewModel.Post
{
    public class ListPostsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? PlaceId { get; set; }
        public string? PlaceName { get; set; }
        public DateTime? VisitedDate { get; set; }
        public List<ListPostImagesViewModel>? Images { get; set; }
        public List<ListPostCommentsViewModel>? Comments { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ListPostImagesViewModel
    {
        public Guid ImageId { get; set; }
        public Guid PostId { get; set; }
        public string Path { get; set; }
    }

    public class ListPostCommentsViewModel
    {
        public Guid PostId { get; set; }
        public Guid ParentId { get; set; }
        public string Content { get; set; }
    }

}
