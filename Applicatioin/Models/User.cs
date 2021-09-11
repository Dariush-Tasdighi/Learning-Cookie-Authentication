namespace Models
{
	public class User : object
	{
		public User() : base()
		{
		}

		public bool IsActive { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string[] Roles { get; set; }

		/// <summary>
		/// New in this Branch!
		/// </summary>
		public string LastSessionId { get; set; }
	}
}
