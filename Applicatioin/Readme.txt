// https://docs.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-5.0

Startup.cs:

	ConfigureServices:

		// using Microsoft.AspNetCore.DataProtection;
		//services.AddDataProtection()
		//	.PersistKeysToFileSystem(directory: "{PATH TO COMMON KEY RING FOLDER}")
		//	.SetApplicationName("SharedCookieApp");

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
