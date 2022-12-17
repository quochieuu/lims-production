using LIMS.Common;
using LIMS.Common.Helpers;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel.Post;
using LIMS.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LIMS.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("lims/post")]
    [Authorize(Roles = Constants.AdminRole)]
    public class PostController : Controller
    {
        private UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlaceRepository _placeRepository;

        public PostController(IPostRepository postRepository,
            UserManager<User> userManager,
            IUserRepository userRepository,
            IPlaceRepository placeRepository)
        {
            _postRepository = postRepository;
            _userManager = userManager;
            _userRepository = userRepository;
            _placeRepository = placeRepository;
        }

        [HttpGet("")]
        public IActionResult Index(string currentFilter, string searchString, int? pageNumber, string? placeId = null)
        {
            ViewBag.ListPlaces = _placeRepository.GetAll();
            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            ViewData["CurrentFilter"] = searchString;

            int pageSize = 10;

            return View(PaginatedList<ListPostsViewModel>.Create(_postRepository.GetAllPaging(currentFilter, searchString, pageNumber, placeId), pageNumber ?? 1, pageSize));
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                await _postRepository.Create(model, username);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdatePostViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                await _postRepository.Update(model, username);
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
                await _postRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}