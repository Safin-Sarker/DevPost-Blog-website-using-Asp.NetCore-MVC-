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

        [HttpGet]
        public async Task<IActionResult>Edit(Guid id)
        {


            //Retrive the result from the repository
            var blogPost=await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (blogPost!=null)
            {
                // map the domain model into the view model

                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeautredImageUrl = blogPost.FeautredImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),

                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),

                };

                return View(model);


            }


            // pass data to view

            return View(null);

        }

        [HttpPost]
        public async Task <IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            // Map view model back to domain model

            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeautredImageUrl = editBlogPostRequest.FeautredImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,
            };

            // Map Tags into domain model

            var selectedTags= new List<Tag>();

            foreach(var selectedTag in editBlogPostRequest.SelectedTags )
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null) 
                    { 
                        selectedTags.Add(foundTag);
                    }

                }
            }

            blogPostDomainModel.Tags= selectedTags;

            // submit information to repository to update

            var updatedBlog=await blogPostRepository.UpdateAsync(blogPostDomainModel);


            // redirect to Get Method
            if (updatedBlog != null)
            {
                //show success notification
                return RedirectToAction("Edit");
            }
             // show failure notification
            return RedirectToAction("Edit");


           




        }



    }
}
