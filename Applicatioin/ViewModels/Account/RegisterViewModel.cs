namespace ViewModels.Account
{
	public class RegisterViewModel : object
	{
		public RegisterViewModel() : base()
		{
		}

		[System.ComponentModel.DataAnnotations.Required]
		public string Username { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]
		public string Password { get; set; }

		[System.ComponentModel.DataAnnotations.Display
			(Name = "Confirm Password")]
		[System.ComponentModel.DataAnnotations.Required]
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]
		[System.ComponentModel.DataAnnotations.Compare
			("Password", ErrorMessage = "Your password and confirm password do not match!")]
		public string ConfirmPassword { get; set; }
	}
}
