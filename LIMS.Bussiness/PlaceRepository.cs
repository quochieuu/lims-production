using LIMS.Common.Helpers;
using LIMS.Data.DataContext;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.PostPlace;
using LIMS.Infrastructure;

namespace LIMS.Bussiness
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly DataDbContext _context;
        public PlaceRepository(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PostPlace> GetAll()
        {
            var item = _context.PostPlaces.Where(x => !x.IsDeleted).OrderByDescending(o => o.CreatedTime).ToList();
            return item;
        }


        public IQueryable<PostPlace> GetAllPaging(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var items = from s in _context.PostPlaces
                        where !s.IsDeleted
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString)
                                       || s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    items = items.OrderBy(s => s.CreatedTime);
                    break;
                case "date_desc":
                    items = items.OrderByDescending(s => s.CreatedTime);
                    break;
                default:
                    items = items.OrderByDescending(s => s.CreatedTime);
                    break;
            }

            return items.AsQueryable();
        }

        public async Task<RepositoryResponse> Create(CreatePlaceViewModel model, string currentUser)
        {
            PostPlace item = new PostPlace()
            {
                Name = model.Name,
                Description = model.Description,
                Image = String.Empty,
                Slug = TextHelper.ToUnsignString(model.Name),
                CreatedTime = DateTime.Now,
                CreatedBy = currentUser,
                LastModifiedBy = currentUser,
                IsDeleted = false
            };
            _context.PostPlaces.Add(item);
            var result = await _context.SaveChangesAsync();
            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async Task<RepositoryResponse> Update(UpdatePlaceViewModel model, string currentUser)
        {
            var item = await _context.PostPlaces.FindAsync(model.Id);

            item.Name = model.Name;
            item.Description = model.Description;
            item.LastModifiedBy = currentUser;
            item.LastModifiedTime = DateTime.Now;
            item.IsDeleted = false;
            _context.PostPlaces.Update(item);
            var result = await _context.SaveChangesAsync();
            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            var item = await _context.PostPlaces.FindAsync(id);
            _context.PostPlaces.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
