using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Repositories
{
	public interface IUserRepository
	{
		public Task<IEnumerable<IdentityUser>> GetAll();
	}
}
