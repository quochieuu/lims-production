using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.Task;

namespace LIMS.Infrastructure
{
    public interface ITaskRepository
    {
        Task<IEnumerable<LIMS.Data.Entities.Task>> GetAll();
        int CountItem(bool? ach = false, bool? str = false, bool? pri = false, bool? cpl = false, string? labelId = null);
        IQueryable<ListTasksViewModel> GetAllPaging(string sortOrder, string currentFilter, string searchString,
            int? pageNumber, bool? ach = false, bool? str = false, bool? pri = false, bool? cpl = false, string? labelId = null);
        Task<RepositoryResponse> Create(CreateTaskViewModel model);
        Task<RepositoryResponse> Update(UpdateTaskViewModel model);
        System.Threading.Tasks.Task Delete(Guid id);
        System.Threading.Tasks.Task<bool> UpdateStart(Guid id);
        System.Threading.Tasks.Task<bool> UpdatePriority(Guid id);
        System.Threading.Tasks.Task<bool> UpdateCompleted(Guid id);
        System.Threading.Tasks.Task Archive(Guid id);
    }
}
