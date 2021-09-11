using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace Utility
{
	public class CustomCookieAuthenticationEvents :
		Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
	{
		public CustomCookieAuthenticationEvents(Applicatioin.Services.IUserService userService)
		{
			UserService = userService;
		}

		public Applicatioin.Services.IUserService UserService { get; set; }

		public override
			async
			System.Threading.Tasks.Task ValidatePrincipal
			(Microsoft.AspNetCore.Authentication.Cookies.CookieValidatePrincipalContext context)
		{
			// **************************************************
			if (context.Principal == null)
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}
			// **************************************************

			var userPrincipal =
				context.Principal;

			// **************************************************
			var foundedUsernameClaim =
				userPrincipal.Claims
				.Where(current => current.Type.ToUpper() ==
					System.Security.Claims.ClaimTypes.Name.ToUpper())
				.FirstOrDefault();

			if (foundedUsernameClaim == null)
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}

			if (string.IsNullOrWhiteSpace(foundedUsernameClaim.Value))
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}
			// **************************************************

			// **************************************************
			string username =
				foundedUsernameClaim.Value;

			Models.User foundedUser =
				UserService.GetUserByUsername(username: username);

			if (foundedUser == null)
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}

			if (foundedUser.IsActive == false)
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}
			// **************************************************
		}

		public async System.Threading.Tasks.Task RejectPrincipalAndSignOutAsync
			(Microsoft.AspNetCore.Authentication.Cookies.CookieValidatePrincipalContext context)
		{
			context.RejectPrincipal();

			// using Microsoft.AspNetCore.Authentication;
			await context.HttpContext.SignOutAsync
				(scheme: Applicatioin.Startup.AuthenticationScheme);
		}
	}
}
