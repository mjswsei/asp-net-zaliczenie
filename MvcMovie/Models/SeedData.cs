using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
	public static void Initialize(IServiceProvider serviceProvider)
	{
		using (var context = new MvcMovieContext(
			serviceProvider.GetRequiredService<
				DbContextOptions<MvcMovieContext>>()))
		{
			if (!context.Director.Any())
				context.Director.AddRange(
					new Director
					{
						Name = "Krzysztof Kieślowski",
						BirthDate = DateTime.Parse("1941-06-27")
					},
					new Director
					{
						Name = "Roman Polański",
						BirthDate = DateTime.Parse("1933-08-18")
					},
					new Director
					{
						Name = "Andrzej Żuławski",
						BirthDate = DateTime.Parse("1940-11-22")
					},
					new Director
					{
						Name = "Andrzej Wajda",
						BirthDate = DateTime.Parse("1926-03-06")
					},
					new Director
					{
						Name = "David Lynch",
						BirthDate = DateTime.Parse("1946-01-20")
					}
				);
				

			if (!context.Genre.Any())


				context.Genre.AddRange(
				new Genre
				{
					Name = "Action"
				},
				new Genre
				{
					Name = "Comedy"
				},
				new Genre
				{
					Name = "Drama"
				},
				new Genre
				{
					Name = "Fantasy"
				},
				new Genre
				{
					Name = "Horror"
				},
				new Genre
				{
					Name = "Mystery"
				},
				new Genre
				{
					Name = "Romance"
				},
				new Genre
				{
					Name = "Thriller"
				}
				);
			context.SaveChanges();
		}
	}
}
