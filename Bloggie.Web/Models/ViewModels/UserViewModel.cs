using System;
using System.Collections.Generic;

namespace Bloggie.Web.Models.ViewModels
{
	public class UserviewModel
	{
		public List<User> Users { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public bool AdminRoleCheckbox { get; set; }
	}
}
