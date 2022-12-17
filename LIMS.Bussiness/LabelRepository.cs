using LIMS.Common.Helpers;
using LIMS.Data.DataContext;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.Label;
using LIMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LIMS.Bussiness
{
    public class LabelRepository : ILabelRepository
    {
        private readonly DataDbContext _context;
        public LabelRepository(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Label> GetAll()
        {
            var item = _context.Labels.Where(x => !x.IsDeleted).OrderByDescending(o => o.CreatedTime).ToList();
            return item;
        }

        public IEnumerable<Label> GetAllJoined()
        {
            var item = (from lbl in _context.Labels
                       join tlb in _context.TaskLabels
                        on lbl.Id equals tlb.LabelId
                       join tsk in _context.Tasks
                        on tlb.TaskId equals tsk.Id
                       where !lbl.IsDeleted
                       orderby lbl.CreatedTime descending
                       select lbl).Distinct().ToList();
            return item;
        }

        public IQueryable<Label> GetAllPaging(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var items = from s in _context.Labels
                        where !s.IsDeleted
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString)
                                       || s.Color.Contains(searchString));
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

        public async Task<RepositoryResponse> Create(CreateLabelViewModel model)
        {
            Label item = new Label()
            {
                Name = model.Name,
                Slug = TextHelper.ToUnsignString(model.Name),
                Color = model.Color,
                CreatedTime = DateTime.Now,
                CreatedBy = "root",
                LastModifiedBy = "root",
                IsDeleted = false
            };
            _context.Labels.Add(item);
            var result = await _context.SaveChangesAsync();
            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async Task<RepositoryResponse> Update(UpdateLabelViewModel model)
        {
            var item = await _context.Labels.FindAsync(model.Id);

            item.Name = model.Name;
            item.Color = model.Color;
            item.LastModifiedBy = "root";
            item.LastModifiedTime = DateTime.Now;
            item.IsDeleted = false;
            _context.Labels.Update(item);
            var result = await _context.SaveChangesAsync();
            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            var item = await _context.Labels.FindAsync(id);
            _context.Labels.Remove(item);
            await _context.SaveChangesAsync();
        }

    }
}
