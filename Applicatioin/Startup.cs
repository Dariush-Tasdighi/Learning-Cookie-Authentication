using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Applicatioin
{
	public class Startup : object
	{
		#region Constants
		public const string AllowAllOrigins = "AllowAllOrigins";

		//public const string AuthenticationScheme =
		//	Microsoft.AspNetCore.Authentication.Cookies
		//	.CookieAuthenticationDefaults.AuthenticationScheme;
		public const string AuthenticationScheme = "Identity.Application";
		#endregion /Constants

		public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

		public void ConfigureServices
			(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			services.AddControllersWithViews();

			// **************************************************
			services.Configure<Microsoft.AspNetCore.Builder.CookiePolicyOptions>(options =>
			{
				options.MinimumSameSitePolicy =
					Microsoft.AspNetCore.Http.SameSiteMode.Lax;

				options.HttpOnly =
					Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;

				options.Secure =
					Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;

				//options.ConsentCookie =

				// This lambda determines whether user consent for
				// non-essential cookies is needed for a given request.
				//options.CheckConsentNeeded = context => true;

				//options.OnAppendCookie
				//options.OnDeleteCookie
			});
			// **************************************************

			// **************************************************
			// using Microsoft.Extensions.DependencyInjection;
			services
				.AddAuthentication(current =>
				{
					current.DefaultScheme = AuthenticationScheme;
					current.DefaultSignInScheme = AuthenticationScheme;
					current.DefaultForbidScheme = AuthenticationScheme;
					current.DefaultSignOutScheme = AuthenticationScheme;
					current.DefaultChallengeScheme = AuthenticationScheme;
					current.DefaultAuthenticateScheme = AuthenticationScheme;

					//current.Schemes
					//current.SchemeMap
					current.RequireAuthenticatedSignIn = true;
				})
				.AddCookie(authenticationScheme: AuthenticationScheme,
					options =>
					{
						options.ClaimsIssuer = "DTAT Security Framework"; // Default: null

						options.SlidingExpiration = true; // Default: true
						options.ExpireTimeSpan =
							new System.TimeSpan(hours: 0, minutes: 20, seconds: 0); // Default: 14 Days

						options.ReturnUrlParameter = "ReturnUrl"; // Default: "ReturnUrl"

						options.LoginPath =
							new Microsoft.AspNetCore.Http.PathString(value: "/Account/Login");

						options.LogoutPath =
							new Microsoft.AspNetCore.Http.PathString(value: "/Account/Logout");

						options.AccessDeniedPath =
							new Microsoft.AspNetCore.Http.PathString(value: "/Account/AccessDenied");

						var value01 = options.Cookie;
						var value02 = options.CookieManager;
						var value03 = options.DataProtectionProvider;

						var value04 = options.EventsType;
						var value05 = options.SessionStore;
						var value06 = options.TicketDataFormat;

						var value07 = options.ForwardForbid;
						var value08 = options.ForwardSignIn;
						var value09 = options.ForwardSignOut;
						var value10 = options.ForwardChallenge;
						var value11 = options.ForwardAuthenticate;
						var value12 = options.ForwardDefaultSelector;

						options.Validate();
					});
			// **************************************************
		}

		public void Configure
			(Microsoft.AspNetCore.Builder.IApplicationBuilder app,
			Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			// **************************************************
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseAuthorization();
			// **************************************************

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
