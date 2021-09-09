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
			var userPrincipal =
				context.Principal;

			// **************************************************
			var foundedUsernameClaim =
				userPrincipal.Claims
				.Where(current => current.Type.ToUpper() == System.Security.Claims.ClaimTypes.Name.ToUpper())
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

			if ((foundedUser == null) || (foundedUser.IsActive == false))
			{
				await RejectPrincipalAndSignOutAsync(context: context);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(foundedUser.LastSessionId))
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}

			var foundedLastSessionIdClaim =
				userPrincipal.Claims
				.Where(current => current.Type.ToUpper() == nameof(foundedUser.LastSessionId).ToUpper())
				.FirstOrDefault();

			if (foundedLastSessionIdClaim == null)
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}

			if (string.IsNullOrWhiteSpace(foundedLastSessionIdClaim.Value))
			{
				await RejectPrincipalAndSignOutAsync(context: context);
				return;
			}

			if (foundedLastSessionIdClaim.Value.ToUpper() != foundedUser.LastSessionId.ToUpper())
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
				(Microsoft.AspNetCore.Authentication.Cookies
				.CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
