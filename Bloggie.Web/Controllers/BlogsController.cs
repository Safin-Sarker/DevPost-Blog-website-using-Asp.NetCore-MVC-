﻿using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
		private readonly IBlogPostLikeRepository blogPostLikeRepository;

		public BlogsController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
		}

        [HttpGet("blogs/{urlHandle}")]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost= await blogPostRepository.GetByUrlHandleAsync(urlHandle);

            var blogDetailsModel = new BlogDetailsView();

            if (blogPost!= null) 
            {
				var totalLikes=await blogPostLikeRepository.GetTotalLikes(blogPost.Id);
				blogDetailsModel = new BlogDetailsView
                {
                    Id=blogPost.Id,
                    Content=blogPost.Content,
                    PageTitle=blogPost.PageTitle,
                    Author=blogPost.Author,
                    FeautredImageUrl=blogPost.FeautredImageUrl,
                    Heading=blogPost.Heading,
                    PublishedDate=blogPost.PublishedDate,
                    ShortDescription=blogPost.ShortDescription,
                    UrlHandle=blogPost.UrlHandle,
                    Visible=blogPost.Visible,
                    Tags=blogPost.Tags,
					TotalLikes = totalLikes

                };



                
            }

            return View(blogDetailsModel);
        }
    }
}
