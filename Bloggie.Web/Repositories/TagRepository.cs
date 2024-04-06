﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery)
        {
            var query =  bloggieDbContext.Tags.AsQueryable();
            
            //Filtering
            if(searchQuery is not null)
            {
                query=query.Where(x=>x.Name.Contains(searchQuery)||
                                      x.DisplayName.Contains(searchQuery));
            }



            return await query.ToListAsync();

            
            //return await bloggieDbContext.Tags.ToListAsync();
        }


        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync(); 
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(id);

            if(existingTag != null)
            {
                bloggieDbContext.Tags.Remove(existingTag);
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;

            }

            return null;
        }

      

        public Task<Tag?> GetAsync(Guid id)
        {
            return  bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if(existingTag != null) 
            { 
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
