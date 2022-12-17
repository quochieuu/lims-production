using LIMS.Common.Helpers;
using LIMS.Data.DataContext;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.Task;
using LIMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LIMS.Bussiness
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataDbContext _context;
        public TaskRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LIMS.Data.Entities.Task>> GetAll()
        {
            var item = await _context.Tasks.Where(x => !x.IsDeleted).OrderByDescending(o => o.CreatedTime).ToListAsync();
            return item;
        }

        public int CountItem(bool? ach = false, bool? str = false, bool? pri = false, bool? cpl = false, string? labelId = null)
        {
            var item = (from x in _context.Tasks
                        let lId = (from lt in _context.TaskLabels
                                   join t in _context.Labels
                                       on lt.LabelId equals t.Id
                                   where !lt.IsDeleted && !t.IsDeleted && lt.TaskId == x.Id
                                   select t.Id).ToList()
                    where !x.IsDeleted 
                    && (ach == true ? x.IsArchived : !x.IsArchived)
                    && (str == false || x.IsStarred)
                    && (pri == false || x.IsPriority)
                    && (cpl == false || x.IsCompleted)
                    && (string.IsNullOrEmpty(labelId) || lId.Any(x => labelId.Contains(x.ToString())))
                    select x
            ).Count();
            return item;
        }

        public IQueryable<ListTasksViewModel> GetAllPaging(string sortOrder, string currentFilter, string searchString, 
            int? pageNumber, bool? ach = false, bool? str = false, bool? pri = false, bool? cpl = false, string? labelId = null)
        {
            var items = (from s in _context.Tasks
                         let lId = (from lt in _context.TaskLabels
                                    join t in _context.Labels
                                        on lt.LabelId equals t.Id 
                                    where !lt.IsDeleted && !t.IsDeleted && lt.TaskId == s.Id
                                    select t.Id).ToList()
                         where !s.IsDeleted 
                                && (ach == true ? s.IsArchived : !s.IsArchived) 
                                && (str == false || s.IsStarred)
                                && (pri == false || s.IsPriority)
                                && (cpl == false || s.IsCompleted)
                                && (string.IsNullOrEmpty(labelId) || lId.Any(x => labelId.Contains(x.ToString())))
                         select new ListTasksViewModel
                         {
                             Id = s.Id,
                             Title = s.Title,
                             Content = s.Content,
                             Deadline = s.Deadline,
                             SortOrder = s.SortOrder,
                             CreatedBy = s.CreatedBy,
                             CreatedTime = s.CreatedTime,
                             IsCompleted = s.IsCompleted,
                             IsOutOfDate = s.IsOutOfDate,
                             IsArchived = s.IsArchived,
                             IsPriority = s.IsPriority,
                             IsDeleted = s.IsDeleted,
                             IsStarred = s.IsStarred,
                             LastModifiedBy = s.LastModifiedBy,
                             LastModifiedTime = s.LastModifiedTime,
                             labels = (from lt in _context.TaskLabels
                                       join t in _context.Labels
                                           on lt.LabelId equals t.Id into joinTaskLabel
                                       from jTL in joinTaskLabel.DefaultIfEmpty()
                                       where !lt.IsDeleted && !jTL.IsDeleted && lt.TaskId == s.Id
                                       select new LabelDto
                                       {
                                           Color = jTL.Color,
                                           Id = jTL.Id,
                                           Name = jTL.Name
                                       }).ToList()
                         }).AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Title.Contains(searchString)
                                       || s.Content.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Title);
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

        public async Task<RepositoryResponse> Create(CreateTaskViewModel model)
        {
            LIMS.Data.Entities.Task item = new LIMS.Data.Entities.Task()
            {
                Title = model.Title,
                Content = model.Content,
                Deadline = model.Deadline,
                SortOrder = model.SortOrder,
                IsCompleted = false,
                IsPriority = false,
                IsStarred = false,
                IsArchived = false,
                IsOutOfDate = false,
                CreatedTime = DateTime.Now,
                CreatedBy = "root",
                LastModifiedBy = "root",
                IsDeleted = false
            };
            _context.Tasks.Add(item);
            var result = await _context.SaveChangesAsync();

            if(model.Labels != null)
            {
                // Split string by comma
                var values = model.Labels.ToLower().Trim().Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();

                    // Check label name in label table
                    // Compare in slug
                    var checkLabel = _context.Labels.Where(x => x.Slug == TextHelper.ToUnsignString(values[i]));

                    // Set local guid
                    Guid labelId = new Guid();
                    if (checkLabel.Count() > 0)
                    {
                        // Try to get label Id
                        var labelName = checkLabel.FirstOrDefault();

                        if (labelName != null)
                            labelId = labelName.Id;
                    } else
                    {
                        // Insert into Label
                        labelId = this.InsertIntoLabel(values[i]);
                    }
                    

                    // Insert into Temp table
                    this.InsertIntoTaskLabel(labelId, item.Id);
                }
            }

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async Task<RepositoryResponse> Update(UpdateTaskViewModel model)
        {
            var item = await _context.Tasks.FindAsync(model.Id);

            item.Title = model.Title;
            item.Content = model.Content;
            item.Deadline = model.Deadline;
            item.SortOrder = model.SortOrder;
            item.IsCompleted = model.IsCompleted;
            item.IsPriority = model.IsPriority;
            item.IsStarred = model.IsStarred;
            item.LastModifiedBy = "root";
            item.LastModifiedTime = DateTime.Now;
            item.IsDeleted = false;
            _context.Tasks.Update(item);
            var result = await _context.SaveChangesAsync();
            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            // remove in temp first
            var temps = await _context.TaskLabels.Where(x => x.TaskId == id).ToListAsync();
            foreach (var temp in temps)
            {
                _context.TaskLabels.Remove(temp);
                await _context.SaveChangesAsync();
            }

            var item = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<bool> UpdateStart(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            task.IsStarred = !task.IsStarred;
            _context.Update(task);
            await _context.SaveChangesAsync();

            return !task.IsStarred;
        }

        public async System.Threading.Tasks.Task<bool> UpdatePriority(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            task.IsPriority = !task.IsPriority;
            _context.Update(task);
            await _context.SaveChangesAsync();

            return !task.IsPriority;
        }

        public async System.Threading.Tasks.Task<bool> UpdateCompleted(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            task.IsCompleted = !task.IsCompleted;
            _context.Update(task);
            await _context.SaveChangesAsync();

            return !task.IsPriority;
        }

        public async System.Threading.Tasks.Task Archive(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            task.IsArchived = !task.IsArchived;
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        // internal function
        internal Guid InsertIntoLabel(string name)
        {
            Label label = new Label()
            {
                Name = name,
                Slug = TextHelper.ToUnsignString(name),
                Color = "#34a1eb",
                CreatedTime = DateTime.Now,
                CreatedBy = "root",
                LastModifiedBy = "root",
                IsDeleted = false
            };
            _context.Labels.Add(label);
            _context.SaveChanges();

            return label.Id;
        }

        internal void InsertIntoTaskLabel(Guid labelId, Guid taskId)
        {
            TaskLabel labelTask = new TaskLabel()
            {
                TaskId = taskId,
                LabelId = labelId,
                CreatedTime = DateTime.Now,
                CreatedBy = "root",
                LastModifiedBy = "root",
                IsDeleted = false
            };
            _context.TaskLabels.Add(labelTask);
            _context.SaveChanges();
        }
    }
}
