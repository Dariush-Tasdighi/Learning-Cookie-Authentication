using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace Applicatioin.Utility
{
	public class CustomCookieAuthenticationEvents :
		Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
	{
		public CustomCookieAuthenticationEvents(Services.IUserService userService)
		{
			UserService = userService;
		}

		public Services.IUserService UserService { get; set; }

		public override
			async
			System.Threading.Tasks.Task ValidatePrincipal
			(Microsoft.AspNetCore.Authentication.Cookies.CookieValidatePrincipalContext context)
		{
			var userPrincipal = context.Principal;

			var foundedClaim =
				userPrincipal.Claims
				.Where(current => current.Type == System.Security.Claims.ClaimTypes.Name)
				.FirstOrDefault();

			if (foundedClaim == null)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(foundedClaim.Value))
			{
				return;
			}

			string username =
				foundedClaim.Value;

			Models.User foundedUser =
				UserService.GetUserByUsername(username: username);

			if ((foundedUser == null) || (foundedUser.IsActive == false))
			{
				context.RejectPrincipal();

				// using Microsoft.AspNetCore.Authentication;
				await context.HttpContext.SignOutAsync(
					Microsoft.AspNetCore.Authentication.Cookies
					.CookieAuthenticationDefaults.AuthenticationScheme);
			}
		}
	}
}
