namespace Applicatioin.Services
{
	public interface IUserService
	{
		void DisableUser(string username);

		Models.User GetUserByUsername(string username);
	}
}
