using LIMS.Common.Helpers;
using LIMS.Data.ViewModel.Task;
using LIMS.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace LIMS.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("lims/task")]
    public class TaskController : Controller
    {
        private readonly ILabelRepository _labelRepository;
        private readonly ITaskRepository _taskRepository;
        public TaskController(ILabelRepository labelRepository, ITaskRepository taskRepository)
        {
            _labelRepository = labelRepository;
            _taskRepository = taskRepository;
        }

        [HttpGet("")]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, bool? ach = false, bool? str = false, bool? pri = false, bool? cpl = false, string? labelId = null)
        {
            var listLables = _labelRepository.GetAll();
            ViewBag.ListLabels = listLables;

            int noTask = _taskRepository.CountItem(ach, str, pri, cpl, labelId);
            ViewBag.NoTask = noTask;

            var labels = _labelRepository.GetAllJoined();
            ViewBag.Labels = labels;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            ViewData["CurrentFilter"] = searchString;

            int pageSize = 10;

            return View(PaginatedList<ListTasksViewModel>.Create(_taskRepository.GetAllPaging(sortOrder, currentFilter, searchString, pageNumber, ach, str, pri, cpl, labelId), pageNumber ?? 1, pageSize));
        }

        [HttpGet("labels")]
        public IActionResult ListLabels()
        {
            return Ok(_labelRepository.GetAll());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            try
            {
                await _taskRepository.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _taskRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("archive/{id}")]
        public async Task<IActionResult> Archive(Guid id)
        {
            try
            {
                await _taskRepository.Archive(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("update-start/{id}")]
        public async Task<IActionResult> UpdateStart(Guid id)
        {
            try
            {
                var result = await _taskRepository.UpdateStart(id);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("update-priority/{id}")]
        public async Task<IActionResult> UpdatePriority(Guid id)
        {
            try
            {
                var result = await _taskRepository.UpdatePriority(id);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("update-completed/{id}")]
        public async Task<IActionResult> UpdateCompleted(Guid id)
        {
            try
            {
                var result = await _taskRepository.UpdateCompleted(id);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
