using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[MinLength(6,ErrorMessage ="Password contains must 6 Character")]
		public string Password { get; set; }

		public string? ReturnUrl { get; set; }
	}
}
