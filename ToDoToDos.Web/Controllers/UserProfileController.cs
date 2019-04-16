using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ToDoToDos.Web.Models;

namespace ToDoToDos.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserProfileController : ControllerBase
	{
		private UserManager<ApplicationUser> _userManager;

		public UserProfileController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		// GET : /api/UserProfile
		[HttpGet]
		[Authorize]
		public async Task<object> GetUserProfile()
		{
			string userId = User.Claims.First(c => c.Type == "UserId").Value;

			var user = await _userManager.FindByIdAsync(userId);

			return new
			{
				user.FullName,
				user.Email,
				user.UserName
			};
		}
	}
}
