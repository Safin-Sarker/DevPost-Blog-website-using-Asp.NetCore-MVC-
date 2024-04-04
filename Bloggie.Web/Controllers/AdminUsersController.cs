using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
	public class AdminUsersController : Controller
	{
		private readonly IUserRepository userRepository;

		public AdminUsersController(IUserRepository userRepository)
        {
			this.userRepository = userRepository;
		}

        public async Task<IActionResult> List()
		{
			var users=await userRepository.GetAllAsync();

			var usersViewModel = new UserviewModel();
			usersViewModel.Users = new List<User>();

			foreach(var user in users)
			{
				usersViewModel.Users.Add(new Models.ViewModels.User 
				{ 
					Id =Guid.Parse(user.Id),
					UserName=user.UserName,
					Email=user.Email


				});

			}




			return View(usersViewModel);
		}
	}
}
