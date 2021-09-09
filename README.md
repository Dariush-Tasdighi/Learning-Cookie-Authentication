# Learning Cookie Authentication

Learning cookie authentication and authorization without ASP.NET Core Identity - C# - ASP.NET Core MVC - User (Users) - Role (Roles) – Claim (Claims)

## First Step Branch:

Just cookie authentication and authorization with (In this step, user passwords is not important):

* Users:

	* User01
	* User02 (Roles: Manager)
	* User03 (Roles: Administrator)
	* User04 (Roles: Manager, Administrator)

* Home Controller Actions:

	* [AllowAnonymous] Index
	* [Authorize] SecuredAction1
	* [Authorize(Roles = "Manager")] SecuredAction2
	* [Authorize(Roles = "Administrator")] SecuredAction3
	* [Authorize(Roles = "Administrator, Manager")] SecuredAction4

## In Second Step Branch:

* We craete IUserService interface and UserService class
* We create CustomCookieAuthenticationEvents class
* There is some moch users (all user passwords are 1234512345):

* Users
	* User01 (Active)
	* User02 (Active) (Roles: Manager)
	* User03 (Active) (Roles: Administrator)
	* User04 (Active) (Roles: Manager, Administrator)
	* User05 (Active)

Administrators can disable User02 (By menu item)
After some administrator disable User02, the user (User02) can not work with the site and should login again!
In brief, in this step, after user has been disabled, the user can not login and if the user is working with site such as authorized user, the user will be sign out automatically.

## In Thrid Step Branch

User can not login twise! We control user logins with a LastSessionId property in user model. 
