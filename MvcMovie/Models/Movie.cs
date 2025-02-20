using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Movie
{
	public int Id { get; set; }

	// walidacje
	[StringLength(60, MinimumLength = 3), Required]
	public string Title { get; set; }

	[Display(Name = "Release Date"), DataType(DataType.Date), Required]
	public DateTime ReleaseDate { get; set; } = DateTime.Now;

	[Range(1, 100), DataType(DataType.Currency)]
	[Column(TypeName = "decimal(18)")]
	public decimal Price { get; set; }

	[Required]
	public Genre Genre { get; set; }

	[Required]
	public Director Director { get; set; }

	[StringLength(30)]
	public string Rating { get; set; }
}
