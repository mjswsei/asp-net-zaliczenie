using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
	public class Genre
	{
		public int Id { get; set; }

		[Required, StringLength(30)]
		public string Name { get; set; }

		//public ICollection<Movie> Movies { get; set; }
	}
}
