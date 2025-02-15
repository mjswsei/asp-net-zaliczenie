using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
	public class MvcMovieUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
