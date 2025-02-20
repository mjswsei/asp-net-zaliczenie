using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models
{
	public class Director
	{
		public int Id { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		[Display(Name = "Date of Birth"), DataType(DataType.Date)]
		public DateTime BirthDate { get; set; } = DateTime.Now;

		//public ICollection<Movie> Movies { get; set; }
	}
}
