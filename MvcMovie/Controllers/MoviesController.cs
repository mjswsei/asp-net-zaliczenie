using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

		// GET: Movies
		public async Task<IActionResult> Index(string movieGenre, string searchString)
		{
			if (_context.Movie == null)
			{
				return Problem("Entity set 'MvcMovieContext.Movie' is null.");
			}

			// linq to get list of genres from db
			//IQueryable<string> genreQuery = from m in _context.Movie
			//								orderby m.Genre
			//								select m.Genre;
			var movies = from m in _context.Movie
                        // join genre in _context.Genre on m.Genre 
						 select m;
			
			if (!string.IsNullOrEmpty(searchString))
			{
				movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
			}

			if (!string.IsNullOrEmpty(movieGenre))
			{
				//movies = movies.Where(x => x.Genre == movieGenre);
			}

			var movieGenreVM = new MovieGenreViewModel
			{
				//Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
				Movies = await movies.Include(m => m.Genre).Include(m => m.Director).ToListAsync()
			};

			return View(movieGenreVM);
		}

		// GET: Movies/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var movie = await _context.Movie.
				Include(m => m.Genre).Include(m => m.Director).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

		// GET: Movies/Create
		[Authorize]
		public IActionResult Create()
        {
            //ViewBag.GenreId = new SelectList(_context.Genre, "Id", "Name");
            //ViewBag.DirectorId = new SelectList(_context.Director, "Id", "Name");
            var viewModel = new MovieCreateViewModel();
            viewModel.Genres = new SelectList(_context.Genre, "Id", "Name");
			viewModel.Directors = new SelectList(_context.Director, "Id", "Name");
			return View(viewModel);
        }

		// POST: Movies/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(MovieCreateViewModel movieViewModel)
		{
			if (ModelState.IsValid)
			{
				var movie = new Movie();
				movie.Title = movieViewModel.Title;
				movie.ReleaseDate = movieViewModel.ReleaseDate;
				movie.Price = movieViewModel.Price;
                //
				movie.Genre = _context.Genre.FirstOrDefault(g => g.Id == movieViewModel.GenreId);
				movie.Director = _context.Director.FirstOrDefault(d => d.Id == movieViewModel.DirectorId);
				movie.Rating = movieViewModel.Rating;
				_context.Add(movie);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(movieViewModel);
		}

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var movie = await _context.Movie.FindAsync(id);
			var movie = await _context.Movie.
				Include(m => m.Genre).Include(m => m.Director).FirstOrDefaultAsync(m => m.Id == id);
			if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

		// GET: Movies/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.
                Include(m => m.Genre).Include(m => m.Director).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

		// POST: Movies/Delete/5
		[Authorize]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
