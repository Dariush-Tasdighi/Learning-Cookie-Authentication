namespace ViewModels.Account
{
	public class LoginViewModel : object
	{
		public LoginViewModel() : base()
		{
		}

		public string ReturnUrl { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		public string Username { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]
		public string Password { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		public bool RememberMe { get; set; }
	}
}
