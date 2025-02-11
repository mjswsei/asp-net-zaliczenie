using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
	public class MvcMovieUser : IdentityUser
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
