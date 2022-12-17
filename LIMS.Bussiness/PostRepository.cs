using LIMS.Common;
using LIMS.Common.Helpers;
using LIMS.Data.DataContext;
using LIMS.Data.Entities;
using LIMS.Data.ViewModel;
using LIMS.Data.ViewModel.Post;
using LIMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LIMS.Bussiness
{
    public class PostRepository : IPostRepository
    {
        private readonly DataDbContext _context;
        public PostRepository(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAll()
        {
            var item = _context.Posts.Where(x => !x.IsDeleted).OrderByDescending(o => o.CreatedTime).ToList();
            return item;
        }

        public IQueryable<ListPostsViewModel> GetAllPaging(string currentFilter, string searchString, int? pageNumber, string? placeId = null)
        {

            var items = (from pt in _context.Posts
                         join pl in _context.PostPlaces 
                            on pt.PlaceId equals pl.Id into joinPlace
                            from jPL in joinPlace.DefaultIfEmpty()
                        where !pt.IsDeleted && !jPL.IsDeleted
                        && (string.IsNullOrEmpty(placeId) || pt.PlaceId == Guid.Parse(placeId))
                         orderby pt.VisitedDate descending
                        select new ListPostsViewModel()
                        {
                            Id = pt.Id,
                            Title = pt.Title,
                            Content = pt.Content,
                            PlaceId = pt.PlaceId,
                            VisitedDate = pt.VisitedDate,
                            PlaceName = jPL.Name,
                            CreatedBy = pt.CreatedBy,
                            CreatedTime = pt.CreatedTime,
                            LastModifiedBy = pt.LastModifiedBy,
                            LastModifiedTime = pt.LastModifiedTime,
                            IsDeleted = pt.IsDeleted,
                            Images = (from pimg in _context.PostImages
                                      where pimg.PostId == pt.Id && !pimg.IsDeleted && !pt.IsDeleted
                                      orderby pimg.CreatedTime descending
                                      select new ListPostImagesViewModel()
                                      {
                                          PostId = pimg.PostId,
                                          ImageId = pimg.Id,
                                          Path = pimg.Path
                                      }).ToList(),
                            Comments = (from pcmt in _context.PostComments
                                        where pcmt.PostId == pt.Id && !pcmt.IsDeleted && !pt.IsDeleted
                                        orderby pcmt.CreatedTime descending
                                        select new ListPostCommentsViewModel()
                                        {
                                            PostId = pcmt.PostId,
                                            Content = pcmt.Content,
                                            ParentId = pcmt.ParentId
                                        }).ToList(),
                        });

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Title.Contains(searchString)
                                       || s.Title.Contains(searchString));
            }

            return items.AsQueryable();
        }

        public async Task<RepositoryResponse> Create(CreatePostViewModel model, string currentUser)
        {
            string IMAGE_PATH = Constants.PostImagePath;

            // Create post
            Post item = new Post()
            {
                Title = model.Title,
                PlaceId = model.PlaceId,
                Content = model.Content,
                VisitedDate = DateTime.Parse(model.VisitedDate),
                CreatedTime = DateTime.Now,
                CreatedBy = currentUser,
                LastModifiedBy = currentUser,
                IsDeleted = false
            };
            _context.Posts.Add(item);
            var result = await _context.SaveChangesAsync();

            if(model.Images != null && model.Images.Count() > 0)
            {
                foreach (var img in model.Images)
                {
                    var imgUpload = UploadImage.UploadImageFile(img, IMAGE_PATH);
                    // add in image
                    PostImage image = new PostImage()
                    {
                        PostId = item.Id,
                        Path = imgUpload,
                        CreatedTime = DateTime.Now,
                        CreatedBy = currentUser,
                        LastModifiedBy = currentUser,
                        IsDeleted = false
                    };
                    _context.PostImages.Add(image);
                    await _context.SaveChangesAsync();
                }
            }

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async Task<RepositoryResponse> Update(UpdatePostViewModel model, string currentUser)
        {
            var item = await _context.Posts.FindAsync(model.Id);

            item.Title = model.Title;
            item.Content = model.Content;
            item.PlaceId = model.PlaceId;
            item.VisitedDate = model.VisitedDate;
            item.LastModifiedBy = currentUser;
            item.LastModifiedTime = DateTime.Now;
            item.IsDeleted = false;
            _context.Posts.Update(item);
            var result = await _context.SaveChangesAsync();
            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id,
            };
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            // delete images
            var imgs = await _context.PostImages.Where(x => x.PostId == id).ToListAsync();
            foreach (var img in imgs)
            {
                _context.PostImages.Remove(img);
            }

            var item = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
