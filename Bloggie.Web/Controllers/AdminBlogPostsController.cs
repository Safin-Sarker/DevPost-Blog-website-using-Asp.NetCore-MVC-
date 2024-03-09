using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository , IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }



        [HttpGet]
        public async Task<IActionResult> Add() 
        {

            //get tags from repository

            var tags= await tagRepository.GetAllAsync();

            var mode = new AddBlogPostRequest
            {
                Tags=tags.Select(X => new SelectListItem { Text=X.Name, Value= X.Id.ToString()  })
            };



            return View(mode);
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {

            var blogpost = new BlogPost()
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle= addBlogPostRequest.PageTitle,
                Content =addBlogPostRequest.Content,
                ShortDescription=addBlogPostRequest.ShortDescription,
                FeautredImageUrl=addBlogPostRequest.FeautredImageUrl,
                UrlHandle=addBlogPostRequest.UrlHandle,
                PublishedDate=addBlogPostRequest.PublishedDate,
                Author=addBlogPostRequest.Author,
                Visible=addBlogPostRequest.Visible,
                

             };
            
            //Map Tags from Selected tags
            var SelecTedTags=new List<Tag>();
            foreach(var selectedTagId in addBlogPostRequest.SelectedTags) 
            {
               var selectedTagIdAsGuid= Guid.Parse(selectedTagId);
               var existingTag=await tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null) 
                {
                    SelecTedTags.Add(existingTag);
                }
                
            }

            //mapping back to domain model
            blogpost.Tags = SelecTedTags;




            await blogPostRepository.AddAsync(blogpost);

            return RedirectToAction("Add");

        }

        // show all blog post 
        [HttpGet]
        public async Task<IActionResult> List ()
        {

            var blogpost=await blogPostRepository.GetAllAsync();
            return View(blogpost);
        }



    }
}
