using Bloggie.Web.Models.Domain;
using System.Security.Principal;

namespace Bloggie.Web.Models.ViewModels
{
	public class BlogDetailsView
	{
		public Guid Id { get; set; }

		public string Heading { get; set; }

		public string PageTitle { get; set; }

		public string? Content { get; set; }

		public string ShortDescription { get; set; }

		public string FeautredImageUrl { get; set; }

		public string UrlHandle { get; set; }

		public DateTime PublishedDate { get; set; }

		public string Author { get; set; }

		public bool Visible { get; set; }

		public ICollection<Tag> Tags { get; set; }

		public int TotalLikes { get; set; }

        public bool Liked { get; set; }

		public string CommentDescription { get; set; }

		public IEnumerable<BlogCommentView> Comment { get; set; }

	
    }
}
