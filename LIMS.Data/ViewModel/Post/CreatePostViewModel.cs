using Microsoft.AspNetCore.Http;

namespace LIMS.Data.ViewModel.Post
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? PlaceId { get; set; }
        public string? VisitedDate { get; set; }
        public IFormFile[]? Images { get; set; }
    }

    public class CreatePostImagesViewModel
    {
        public Guid PostId { get; set; }
        public IFormFile Path { get; set; }
    }
}
