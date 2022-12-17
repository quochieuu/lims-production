using LIMS.Data.Entities;

namespace LIMS.Infrastructure
{
    public interface IUserRepository
    {
        Task<User> FindUserByIdAsync(Guid id);
        Task<User> FindUserByUsernameAsync(string username);
        Task<User> FindUserByEmailAsync(string email);
        IEnumerable<User> GetAll();
        IQueryable<User> GetAllPaging(string sortOrder, string currentFilter, string searchString, int? pageNumber);
    }
}
