using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : IdentityDbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
		public DbSet<MvcMovie.Models.Genre> Genre { get; set; } = default!;
		public DbSet<MvcMovie.Models.Director> Director { get; set; } = default!;


		public class MvcMovieUserEntityConfiguration : IEntityTypeConfiguration<MvcMovieUser>
		{
			public void Configure(EntityTypeBuilder<MvcMovieUser> builder)
			{
				builder.Property(u => u.FirstName).HasMaxLength(150);
				builder.Property(u => u.LastName).HasMaxLength(150);
			}
		}
	}
	
}
