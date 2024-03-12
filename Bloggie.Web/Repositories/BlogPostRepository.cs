using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }



        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
          await bloggieDbContext.AddAsync(blogPost);
          await bloggieDbContext.SaveChangesAsync();
          return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog=await bloggieDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null) 
            {
                bloggieDbContext.BlogPosts.Remove(existingBlog);
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await bloggieDbContext.BlogPosts.Include(x=> x.Tags).ToListAsync() ;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingblog= await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==blogPost.Id);

           if (existingblog!=null) 
           {
                existingblog.Id = blogPost.Id;
                existingblog.Heading=blogPost.Heading;
                existingblog.PageTitle=blogPost.PageTitle;
                existingblog.Content=blogPost.Content;
                existingblog.ShortDescription=blogPost.ShortDescription;
                existingblog.Author=blogPost.Author;
                existingblog.FeautredImageUrl=blogPost.FeautredImageUrl;
                existingblog.UrlHandle=blogPost.UrlHandle;
                existingblog.Visible=blogPost.Visible;
                existingblog.PublishedDate=blogPost.PublishedDate;
                existingblog.Tags=blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return existingblog;
           }

            return null;

        }
    }
}
