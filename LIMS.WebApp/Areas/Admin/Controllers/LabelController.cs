using LIMS.Common.Helpers;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel.Label;
using LIMS.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace LIMS.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("lims/label")]
    public class LabelController : Controller
    {
        private readonly ILabelRepository _labelRepository;
        public LabelController(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            ViewData["CurrentFilter"] = searchString;

            int pageSize = 10;

            // View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
            return View(PaginatedList<Label>.Create(_labelRepository.GetAllPaging(sortOrder, currentFilter, searchString, pageNumber), pageNumber ?? 1, pageSize));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateLabelViewModel model)
        {
            try
            {
                await _labelRepository.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateLabelViewModel model)
        {
            try
            {
                await _labelRepository.Update(model);
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
                await _labelRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
