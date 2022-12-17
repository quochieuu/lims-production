using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.Label;

namespace LIMS.Infrastructure
{
    public interface ILabelRepository
    {
        IEnumerable<Label> GetAll();
        IEnumerable<Label> GetAllJoined();
        IQueryable<Label> GetAllPaging(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Task<RepositoryResponse> Create(CreateLabelViewModel model);
        Task<RepositoryResponse> Update(UpdateLabelViewModel model);
        System.Threading.Tasks.Task Delete(Guid id);
    }
}
