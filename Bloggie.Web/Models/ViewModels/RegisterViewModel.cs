using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set;}

		[Required]
		[MinLength(6,ErrorMessage ="Password has to be 6 character")]		
		public string Password { get; set;}
	}
}
