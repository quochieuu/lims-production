using LIMS.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LIMS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using LIMS.Data.ViewModel.BeenLove;
using LIMS.Common.Helpers;
using LIMS.Common;

namespace LIMS.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("lims/been-love")]
    [Authorize(Roles = Constants.AdminRole)]
    public class BeenLoveController : Controller
    {
        private UserManager<User> _userManager;
        private readonly ILoveRepository _loveRepository;
        private readonly IUserRepository _userRepository;

        public BeenLoveController(ILoveRepository loveRepository, 
            UserManager<User> userManager, 
            IUserRepository userRepository)
        {
            _loveRepository = loveRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.AllUsers = _userRepository.GetAll();

            var user = await _userManager.GetUserAsync(User);
            string username = user.UserName;

            var data = await _loveRepository.GetUserLoveEvent(username);

            return View(data);
        }

        [HttpGet("check-been-event-created")]
        public async Task<IActionResult> CheckBeenLoveEventCreated()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                var result = await _loveRepository.CheckUserHasLoveEvent(username);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateLoveEventViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                await _loveRepository.Create(model, username);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("update-couple-avatar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCoupleImage(Guid id, UpdateCoupleImageViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                string IMAGE_PATH = Constants.CoupleAvatarPath;

                if (model.AvatarUser1 != null && model.AvatarUser2 != null)
                {

                    var avatar1 = UploadImage.UploadImageFile(model.AvatarUser1, IMAGE_PATH);
                    var avatar2 = UploadImage.UploadImageFile(model.AvatarUser2, IMAGE_PATH);

                    await _loveRepository.UpdateCoupleImage(id, avatar1, avatar2, username);

                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateLoveEventViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                string username = user.UserName;

                await _loveRepository.Update(model, username);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
