https://docs.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-5.0

Data Projection:

https://github.com/aspnet/Security/issues/1833
https://github.com/dotnet/aspnetcore/issues/19290
https://github.com/dotnet/AspNetCore.Docs/issues/21987
https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/security
https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-5.0

https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/samples?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/community?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/other-logins?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/social-without-identity?view=aspnetcore-5.0

https://apereo.github.io/cas/6.4.x/index.html (JAVA)
https://github.com/blowdart/AspNetAuthenticationWorkshop

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

----------

Consider a situation in which the user's name is updated a decision that
doesn't affect security in any way. If you want to non-destructively update
the user principal, call context.ReplacePrincipal and set the context.ShouldRenew property to true.

----------
