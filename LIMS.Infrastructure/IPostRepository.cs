using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.Post;

namespace LIMS.Infrastructure
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAll();
        Task<RepositoryResponse> Create(CreatePostViewModel model, string currentUser);
        IQueryable<ListPostsViewModel> GetAllPaging(string currentFilter, string searchString, int? pageNumber, string? placeId = null);
        System.Threading.Tasks.Task Delete(Guid id);
        Task<RepositoryResponse> Update(UpdatePostViewModel model, string currentUser);
    }
}
