using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MvcMovie.Models
{
	public class MovieCreateViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Display(Name = "Release Date"), DataType(DataType.Date), Required]
		public DateTime ReleaseDate { get; set; } = DateTime.Now;

		public decimal Price { get; set; }

		//selected genre
		[Required]
		public int GenreId { get; set; }

		[ValidateNever]
		public SelectList Genres { get; set; }

		//selected director
		[Required]
		public int DirectorId { get; set; }

		[ValidateNever]
		public SelectList Directors { get; set; }

		public string Rating { get; set; }


	}
}
