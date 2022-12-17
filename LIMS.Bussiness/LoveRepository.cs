using LIMS.Data.DataContext;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.BeenLove;
using LIMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LIMS.Bussiness
{
    public class LoveRepository : ILoveRepository
    {
        private readonly DataDbContext _context;
        public LoveRepository(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BeenLove> GetAll()
        {
            var item = _context.BeenLoves.Where(x => !x.IsDeleted).OrderByDescending(o => o.CreatedTime).ToList();
            return item;
        }

        public async Task<bool> CheckUserHasLoveEvent(string username)
        {
            var check = await _context.BeenLoves.Where(
                x => (x.Username1 == username || x.Username2 == username)
                && !x.IsDeleted).CountAsync();

            if (check > 0)
                return true;
            return false;
        }

        public async Task<BeenLove> GetUserLoveEvent(string username)
        {
            var loveEvent = await _context.BeenLoves.Where(
                x => (x.Username1 == username || x.Username2 == username)
                && !x.IsDeleted).FirstOrDefaultAsync();

            if(loveEvent != null)
            {
                return loveEvent;
            }
            return new BeenLove();
        }

        public async Task<RepositoryResponse> Create(CreateLoveEventViewModel model, string currentUser)
        {
            BeenLove item = new BeenLove()
            {
                Name = "Been Love Together",
                Content = "Been Love Together",
                AgeUser1 = DateTime.Parse(model.AgeUser1),
                AgeUser2 = DateTime.Parse(model.AgeUser2),
                AvatarUser1 = String.Empty,
                AvatarUser2 = String.Empty,
                Background = String.Empty,
                Username1 = currentUser,
                Username2 = model.Username2,
                User1 = model.User1,
                User2 = model.User2,
                StartDate = model.StartDate,
                CreatedTime = DateTime.Now,
                CreatedBy = currentUser,
                LastModifiedBy = currentUser,
                IsDeleted = false
            };
            _context.BeenLoves.Add(item);
            var result = await _context.SaveChangesAsync();
            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async Task<BeenLove> UpdateCoupleImage(Guid id, string avatar1, string avatar2, string currentUser)
        {
            var item = await _context.BeenLoves.Where(s => s.Id == id && !s.IsDeleted).FirstOrDefaultAsync();
            item.AvatarUser1 = avatar1;
            item.AvatarUser2 = avatar2;
            item.LastModifiedBy = currentUser;
            item.LastModifiedTime = DateTime.Now;

            _context.BeenLoves.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<RepositoryResponse> Update(UpdateLoveEventViewModel model, string currentUser)
        {
            var item = await _context.BeenLoves.FindAsync(model.Id);

            item.StartDate = model.StartDate;
            item.User1 = model.User1;
            item.User2 = model.User2;
            item.AgeUser1 = model.AgeUser1;
            item.AgeUser2 = model.AgeUser2;
            item.Username2 = model.Username2;

            item.LastModifiedBy = currentUser;
            item.LastModifiedTime = DateTime.Now;
            item.IsDeleted = false;

            _context.BeenLoves.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

    }
}
