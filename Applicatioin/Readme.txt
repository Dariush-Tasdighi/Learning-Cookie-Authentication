Startup.cs:

	ConfigureServices:

		// https://docs.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-5.0
		// using Microsoft.AspNetCore.DataProtection;
		//services.AddDataProtection()
		//	.PersistKeysToFileSystem(directory: "{PATH TO COMMON KEY RING FOLDER}")
		//	.SetApplicationName("SharedCookieApp");

		//services.AddAuthentication("Identity.Application")
		//	.AddCookie("Identity.Application", options =>
		//	{
		//		options.Cookie.Name = ".AspNet.SharedCookie";
		//		//options.AccessDeniedPath
		//		//options.ClaimsIssuer
		//		//options.Cookie
		//		//options.CookieManager
		//		//options.DataProtectionProvider
		//		//options.EventsType
		//		//options.ExpireTimeSpan
		//		//options.ForwardAuthenticate
		//		//options.ExpireTimeSpan
		//		//options.ForwardChallenge
		//		//options.LoginPath
		//	});

	Configure:

		//app.UseCookieAuthentication
		//	(new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions()
		//{
		//	AuthenticationScheme = "CookieAuthentication",
		//	LoginPath = new PathString("/Account/Login"),
		//	AccessDeniedPath = new PathString("/Account/Forbidden/"),
		//	AutomaticAuthenticate = true,
		//	AutomaticChallenge = true
		//});
