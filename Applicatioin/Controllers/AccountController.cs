using Microsoft.AspNetCore.Authentication;

namespace Applicatioin.Controllers
{
	public class AccountController : Microsoft.AspNetCore.Mvc.Controller
	{
		public AccountController() : base()
		{
		}

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
			//ModelState.AddModelError(key: null, errorMessage: "Some Error Message!"); // Error!
			//ModelState.AddModelError(key: "", errorMessage: "Some Error Message!");
			//ModelState.AddModelError(key: string.Empty, errorMessage: "Some Error Message!");

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				var claims =
					new System.Collections.Generic.List<System.Security.Claims.Claim>();

				System.Security.Claims.Claim claim;

				// **************************************************
				claim = new System.Security.Claims.Claim
					(type: System.Security.Claims.ClaimTypes.Name, value: viewModel.Username);

				claims.Add(item: claim);
				// **************************************************

				// **************************************************
				switch (viewModel.Username.ToUpper())
				{
					case "DARIUSH":
					{
						claim = new System.Security.Claims.Claim
							(type: System.Security.Claims.ClaimTypes.Role, value: "Manager");

						claims.Add(item: claim);

						break;
					}

					case "ADMIN":
					{
						claim = new System.Security.Claims.Claim
							(type: System.Security.Claims.ClaimTypes.Role, value: "Administrator");

						claims.Add(item: claim);

						break;
					}

					case "MOHSEN":
					{
						claim = new System.Security.Claims.Claim
							(type: System.Security.Claims.ClaimTypes.Role, value: "Manager");

						claims.Add(item: claim);

						claim = new System.Security.Claims.Claim
							(type: System.Security.Claims.ClaimTypes.Role, value: "Administrator");

						claims.Add(item: claim);

						break;
					}

					case "MOHAMMAD":
					{
						// نکته: دقت کنید که مدل ذیل کار نمی‌کند
						claim = new System.Security.Claims.Claim
							(type: System.Security.Claims.ClaimTypes.Role, value: "Administrator, Manager");

						claims.Add(item: claim);

						break;
					}
				}
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				var claimsIdentity =
					new System.Security.Claims.ClaimsIdentity
						(claims: claims, authenticationType:
						Microsoft.AspNetCore.Authentication.Cookies
						.CookieAuthenticationDefaults.AuthenticationScheme);

				var authenticationProperties = new AuthenticationProperties
				{
					// Refreshing the authentication session should be allowed.
					AllowRefresh = true,

					// The time at which the authentication ticket expires.
					// A value set here overrides the ExpireTimeSpan option of
					// CookieAuthenticationOptions set with AddCookie.
					ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(10),

					// Whether the authentication session is persisted across
					// multiple requests. When used with cookies, controls
					// whether the cookie's lifetime is absolute (matching the
					// lifetime of the authentication ticket) or session-based.
					IsPersistent = viewModel.RememberMe,

					// The time at which the authentication ticket was issued.
					IssuedUtc = System.DateTimeOffset.UtcNow,

					// The full path or absolute URI to be
					// used as an http redirect response value.
					RedirectUri = null,
				};

				var claimsPrincipal =
					new System.Security.Claims.ClaimsPrincipal(identity: claimsIdentity);

				// using Microsoft.AspNetCore.Authentication;
				await HttpContext.SignInAsync
					(scheme: Microsoft.AspNetCore.Authentication.Cookies
					.CookieAuthenticationDefaults.AuthenticationScheme,
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
			System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.RedirectToActionResult> Logout()
		{
			// using Microsoft.AspNetCore.Authentication;
			await HttpContext.SignOutAsync
				(scheme: Microsoft.AspNetCore.Authentication.Cookies
				.CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction
				(controllerName: "Account", actionName: "Login");
		}
	}
}
