using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostLikeController : ControllerBase
	{
		private readonly IBlogPostLikeRepository blogPostLikeRepository;

		public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
			this.blogPostLikeRepository = blogPostLikeRepository;
		}



        [HttpPost]
		[Route("Add")]

		public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
		{
			try
			{

				var model = new BlogPostLike()
				{
					BlogPostId = addLikeRequest.BlogPostId,
					UserId = addLikeRequest.UserId,

				};

				await blogPostLikeRepository.AddLikeForBlog(model);

				return Ok();
			}

			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the like.");
			}

		}

		[HttpGet]
		[Route("{blogPostId:Guid}/totalLikes")]
		public async Task <IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostID)
		{
			var totalLikes=await blogPostLikeRepository.GetTotalLikes(blogPostID);

			return Ok(totalLikes);
		}
	}
}
