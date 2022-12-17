using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.BeenLove;

namespace LIMS.Infrastructure
{
    public interface ILoveRepository
    {
        IEnumerable<BeenLove> GetAll();
        Task<bool> CheckUserHasLoveEvent(string username);
        Task<BeenLove> GetUserLoveEvent(string username);
        Task<RepositoryResponse> Create(CreateLoveEventViewModel model, string currentUser);
        Task<BeenLove> UpdateCoupleImage(Guid id, string avatar1, string avatar2, string currentUser);
        Task<RepositoryResponse> Update(UpdateLoveEventViewModel model, string currentUser);
    }
}
