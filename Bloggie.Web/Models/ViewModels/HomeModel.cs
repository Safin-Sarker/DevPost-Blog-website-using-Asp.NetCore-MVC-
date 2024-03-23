using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Models.ViewModels
{
	public class HomeModel
	{
		public IEnumerable<BlogPost>BlogPosts { get; set; }

		public IEnumerable<Tag> Tags {  get; set; }	
	}
}
