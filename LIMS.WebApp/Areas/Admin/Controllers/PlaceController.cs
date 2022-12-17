using LIMS.Common;
using LIMS.Common.Helpers;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel.PostPlace;
using LIMS.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LIMS.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("lims/place")]
    [Authorize(Roles = Constants.AdminRole)]
    public class PlaceController : Controller
    {
        private UserManager<User> _userManager;
        private readonly IPlaceRepository _placeRepository;
        private readonly IUserRepository _userRepository;

        public PlaceController(IPlaceRepository placeRepository,
            UserManager<User> userManager,
            IUserRepository userRepository)
        {
            _placeRepository = placeRepository;
            _userManager = userManager;
            _userRepository = userRepository;
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

            return View(PaginatedList<PostPlace>.Create(_placeRepository.GetAllPaging(sortOrder, currentFilter, searchString, pageNumber), pageNumber ?? 1, pageSize));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePlaceViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                await _placeRepository.Create(model, username);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdatePlaceViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                await _placeRepository.Update(model, username);
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
                await _placeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
