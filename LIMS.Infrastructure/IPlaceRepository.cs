using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.PostPlace;

namespace LIMS.Infrastructure
{
    public interface IPlaceRepository
    {
        IEnumerable<PostPlace> GetAll();
        IQueryable<PostPlace> GetAllPaging(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Task<RepositoryResponse> Create(CreatePlaceViewModel model, string currentUser);
        Task<RepositoryResponse> Update(UpdatePlaceViewModel model, string currentUser);
        System.Threading.Tasks.Task Delete(Guid id);
    }
}
