using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
		private readonly IBlogPostLikeRepository blogPostLikeRepository;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IBlogPostCommentRepository blogPostCommentRepository;

		public BlogsController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser>signInManager,
            UserManager<IdentityUser>userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.blogPostCommentRepository = blogPostCommentRepository;
		}

        [HttpGet("blogs/{urlHandle}")]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost= await blogPostRepository.GetByUrlHandleAsync(urlHandle);

            var blogDetailsModel = new BlogDetailsView();

            if (blogPost!= null) 
            {
				var totalLikes=await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                if(signInManager.IsSignedIn(User))
                {
                    //Get like for this blog for this user
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

                    var userId = userManager.GetUserId(User);

                    if(userId!=null)
                    {
                       var likeFromUser = likesForBlog.FirstOrDefault(x=>x.UserId==Guid.Parse(userId));
                        liked = likeFromUser != null ;
                    }
                }

                //Get Comments for blog

                var blogComentsDomainModel=await blogPostCommentRepository.GetCommentsByBlogIdAsync(blogPost.Id);

                var blogCommentsForView= new List<BlogCommentView>();

                foreach(var comment in blogComentsDomainModel)
                {
                    blogCommentsForView.Add(new BlogCommentView
                    {

                        Description = comment.Description,

						AddDate = comment.DateAdded,

                        UserName=(await userManager.FindByIdAsync(comment.UserId.ToString())).UserName


                    }) ; 


                }



                blogDetailsModel = new BlogDetailsView
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeautredImageUrl = blogPost.FeautredImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked=liked,
                    Comment= blogCommentsForView,

				};



                
            }

            return View(blogDetailsModel);
        }


		[HttpPost("blogs/{urlHandle}")]

		public async Task<IActionResult> Index(BlogDetailsView blogDetailsView)
        {

            if (signInManager.IsSignedIn(User))
            {
                var model = new BlogPostComment
                {
                    BlogPostId = blogDetailsView.Id,
                    Description = blogDetailsView.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
				};



				await blogPostCommentRepository.AddAsync(model);

                return RedirectToAction("Index", "Blogs", new {urlHandle=blogDetailsView.UrlHandle});

			}

            return View();
            


        }

    }
}
