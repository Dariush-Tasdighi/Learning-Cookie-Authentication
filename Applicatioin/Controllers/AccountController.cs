using Microsoft.AspNetCore.Authentication;

namespace Applicatioin.Controllers
{
	public class AccountController : Microsoft.AspNetCore.Mvc.Controller
	{
		public AccountController(Services.IUserService userService) : base()
		{
			UserService = userService;
		}

		public Services.IUserService UserService { get; }

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public Microsoft.AspNetCore.Mvc.ViewResult Login(string returnUrl)
		{
			var viewModel =
				new ViewModels.Account.LoginViewModel
				{
					ReturnUrl = returnUrl,
				};

			return View(model: viewModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpPost]
		[Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public async
			System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult>
			Login(ViewModels.Account.LoginViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				var foundedUser =
					UserService.GetUserByUsername
					(username: viewModel.Username);

				if (foundedUser == null)
				{
					ModelState.AddModelError
						(key: string.Empty, errorMessage:
						"Invalid Username and/or Password!");

					return View(model: viewModel);
				}

				if (foundedUser.Password != viewModel.Password)
				{
					ModelState.AddModelError
						(key: string.Empty, errorMessage:
						"Invalid Username and/or Password!");

					return View(model: viewModel);
				}

				if (foundedUser.IsActive == false)
				{
					ModelState.AddModelError
						(key: string.Empty,
						errorMessage: "Your account is disabled!");

					return View(model: viewModel);
				}

				string lastSessionId =
					System.Guid.NewGuid().ToString();

				foundedUser.LastSessionId = lastSessionId;
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				var claims =
					new System.Collections.Generic
					.List<System.Security.Claims.Claim>();

				System.Security.Claims.Claim claim;

				// **************************************************
				claim =
					new System.Security.Claims.Claim
					(type: System.Security.Claims.ClaimTypes.Name, value: foundedUser.Username);

				claims.Add(item: claim);
				// **************************************************

				// **************************************************
				claim =
					new System.Security.Claims.Claim
					(type: nameof(foundedUser.LastSessionId), value: lastSessionId);

				claims.Add(item: claim);
				// **************************************************

				// **************************************************
				if (foundedUser.Roles != null)
				{
					foreach (var role in foundedUser.Roles)
					{
						claim =
							new System.Security.Claims.Claim
							(type: System.Security.Claims.ClaimTypes.Role, value: role);

						claims.Add(item: claim);
					}
				}
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				var claimsIdentity =
					new System.Security.Claims.ClaimsIdentity
						(claims: claims, authenticationType: Startup.AuthenticationScheme);

				var authenticationProperties =
					new AuthenticationProperties
					{
						// Refreshing the authentication session should be allowed.
						AllowRefresh = true,

						// Whether the authentication session is persisted across
						// multiple requests. When used with cookies, controls
						// whether the cookie's lifetime is absolute (matching the
						// lifetime of the authentication ticket) or session-based.
						IsPersistent =
							viewModel.RememberMe,

						// The time at which the authentication ticket expires.
						// A value set here overrides the ExpireTimeSpan option of
						// CookieAuthenticationOptions set with AddCookie.
						ExpiresUtc =
							viewModel.RememberMe ? System.DateTimeOffset.UtcNow.AddMinutes(20) : null,

						// The time at which the authentication ticket was issued.
						IssuedUtc =
							System.DateTimeOffset.UtcNow,

						// The full path or absolute URI to be
						// used as an http redirect response value.
						RedirectUri = null,
					};

				var claimsPrincipal =
					new System.Security.Claims
					.ClaimsPrincipal(identity: claimsIdentity);

				// using Microsoft.AspNetCore.Authentication;
				await HttpContext.SignInAsync
					(scheme: Startup.AuthenticationScheme,
					principal: claimsPrincipal, properties: authenticationProperties);
				// **************************************************

				if (string.IsNullOrWhiteSpace(viewModel.ReturnUrl) == false)
				{
					return Redirect(url: viewModel.ReturnUrl);
				}
				else
				{
					return RedirectToAction
						(controllerName: "Home", actionName: "Index");
				}
			}

			return View(model: viewModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public Microsoft.AspNetCore.Mvc.ViewResult Register()
		{
			return View();
		}

		[Microsoft.AspNetCore.Mvc.HttpPost]
		[Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public Microsoft.AspNetCore.Mvc.IActionResult
			Register(ViewModels.Account.RegisterViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction
					(controllerName: "Account", actionName: "Login");
			}

			return View(model: viewModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public Microsoft.AspNetCore.Mvc.ViewResult Profile()
		{
			return View();
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public Microsoft.AspNetCore.Mvc.ViewResult AccessDenied(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;

			return View();
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public
			async
			System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.RedirectToActionResult>
			Logout()
		{
			// using Microsoft.AspNetCore.Authentication;
			await HttpContext.SignOutAsync
				(scheme: Startup.AuthenticationScheme);

			return RedirectToAction
				(controllerName: "Account", actionName: "Login");
		}

		[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrator")]
		public Microsoft.AspNetCore.Mvc.RedirectToActionResult DisableUser02()
		{
			UserService.DisableUser(username: "User02");

			return RedirectToAction
				(controllerName: "Home", actionName: "Index");
		}
	}
}
