using Microsoft.AspNetCore.Mvc;

namespace first.Models
{
	public class LoginVM
	{
		[Remote(action: "EmailExist", controller: "Remote")]
		public string Email { get; set; }
		public string Password { get; set; }

	}
}
