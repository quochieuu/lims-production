using Microsoft.AspNetCore.Http;

namespace LIMS.Data.ViewModel.BeenLove
{
    public class UpdateCoupleImageViewModel
    {
        public IFormFile AvatarUser1 { get; set; }
        public IFormFile AvatarUser2 { get; set; }
    }
}
