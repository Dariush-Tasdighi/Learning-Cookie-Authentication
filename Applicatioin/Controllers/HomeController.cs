namespace Applicatioin.Controllers
{
	public class HomeController : Microsoft.AspNetCore.Mvc.Controller
	{
		public HomeController() : base()
		{
		}

		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public Microsoft.AspNetCore.Mvc.ViewResult Index()
		{
			return View();
		}

		[Microsoft.AspNetCore.Authorization.Authorize]
		public Microsoft.AspNetCore.Mvc.ViewResult SecuredAction1()
		{
			return View();
		}

		[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Manager")]
		public Microsoft.AspNetCore.Mvc.ViewResult SecuredAction2()
		{
			return View();
		}

		[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrator")]
		public Microsoft.AspNetCore.Mvc.ViewResult SecuredAction3()
		{
			return View();
		}

		[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrator, Manager")]
		public Microsoft.AspNetCore.Mvc.ViewResult SecuredAction4()
		{
			return View();
		}
	}
}
