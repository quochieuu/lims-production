using LIMS.Data.DataContext;
using LIMS.Data.Entities;
using LIMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LIMS.Bussiness
{
    public class UserRepository : IUserRepository
    {
        private readonly DataDbContext _context;
        public UserRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindUserByIdAsync(Guid id)
        {
            var item = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<User> FindUserByUsernameAsync(string username)
        {
            var item = await _context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
            return item;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var item = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return item;
        }

        public IEnumerable<User> GetAll()
        {
            var item = _context.Users.OrderByDescending(o => o.Id).ToList();
            return item;
        }

        public IQueryable<User> GetAllPaging(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var items = from s in _context.Users
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.UserName.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       || s.Id.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "username_desc":
                    items = items.OrderByDescending(s => s.UserName);
                    break;
                case "email":
                    items = items.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    items = items.OrderByDescending(s => s.Email);
                    break;
                default:
                    items = items.OrderByDescending(s => s.Id);
                    break;
            }

            return items.AsQueryable();
        }

    }
}
