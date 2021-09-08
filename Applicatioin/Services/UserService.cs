using System.Linq;

namespace Applicatioin.Services
{
	public class UserService : object, IUserService
	{
		private static System.Collections.Generic.IList<Models.User> _users;

		private static System.Collections.Generic.IList<Models.User> Users
		{
			get
			{
				if (_users == null)
				{
					_users =
						new System.Collections.Generic.List<Models.User>();
				}

				if (_users.Count == 0)
				{
					Models.User user;

					// **************************************************
					user =
						new Models.User
						{
							IsActive = false,
							Username = "User01",
							Password = "1234512345",

							Roles = null,
						};

					_users.Add(user);
					// **************************************************

					// **************************************************
					user =
						new Models.User
						{
							IsActive = true,
							Username = "User02",
							Password = "1234512345",

							Roles = null,
						};

					_users.Add(user);
					// **************************************************

					// **************************************************
					user =
						new Models.User
						{
							IsActive = true,
							Username = "User03",
							Password = "1234512345",

							Roles = new string[] { "Manager" },
						};

					_users.Add(user);
					// **************************************************

					// **************************************************
					user =
						new Models.User
						{
							IsActive = true,
							Username = "User04",
							Password = "1234512345",

							Roles = new string[] { "Administrator" },
						};

					_users.Add(user);
					// **************************************************

					// **************************************************
					user =
						new Models.User
						{
							IsActive = true,
							Username = "User05",
							Password = "1234512345",

							Roles = new string[] { "Manager", "Administrator" },
						};

					_users.Add(user);
					// **************************************************
				}

				return _users;
			}
		}

		public UserService() : base()
		{
		}

		public Models.User GetUserByUsername(string username)
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				return null;
			}

			var result =
				Users
				.Where(current => current.Username.ToUpper() == username.ToUpper())
				.FirstOrDefault();

			return result;
		}

		public void DisableUser(string username)
		{
			var foundedUser =
				GetUserByUsername(username: username);

			if (foundedUser != null)
			{
				foundedUser.IsActive = false;
			}
		}
	}
}
