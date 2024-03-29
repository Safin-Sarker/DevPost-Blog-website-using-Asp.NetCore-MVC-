using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{


	public class BlogPostLikeRepository : IBlogPostLikeRepository
	{
		private readonly BloggieDbContext bloggieDbContext;

		public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
			this.bloggieDbContext = bloggieDbContext;
		}

		public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
		{
			await bloggieDbContext.AddAsync(blogPostLike);
			await bloggieDbContext.SaveChangesAsync();
			return blogPostLike;
		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
		{
			return await bloggieDbContext.BlogPostLikes.CountAsync(x=>x.BlogPostId == blogPostId);
		}
	}
}
