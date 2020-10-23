﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Models;

namespace MovieStore.Controllers
{
    public class MovieGenresController : Controller
    {
        private readonly MovieStoreContext _context;

        public MovieGenresController(MovieStoreContext context)
        {
            _context = context;
        }

        // GET: MovieGenres
        public async Task<IActionResult> Index()
        {
            var movieStoreContext = _context.MovieGenre.Include(m => m.Genre).Include(m => m.Movie);
            return View(await movieStoreContext.ToListAsync());
        }

        // GET: MovieGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenre = await _context.MovieGenre
                .Include(m => m.Genre)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieGenre == null)
            {
                return NotFound();
            }

            return View(movieGenre);
        }

        // GET: MovieGenres/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Name");
            return View();
        }

        // POST: MovieGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,GenreId")] MovieGenre movieGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type", movieGenre.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Name", movieGenre.MovieId);
            return View(movieGenre);
        }

        // GET: MovieGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenre = await _context.MovieGenre.FindAsync(id);
            if (movieGenre == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type", movieGenre.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Name", movieGenre.MovieId);
            return View(movieGenre);
        }

        // POST: MovieGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,GenreId")] MovieGenre movieGenre)
        {
            if (id != movieGenre.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieGenreExists(movieGenre.MovieId))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type", movieGenre.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Name", movieGenre.MovieId);
            return View(movieGenre);
        }

        // GET: MovieGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenre = await _context.MovieGenre
                .Include(m => m.Genre)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieGenre == null)
            {
                return NotFound();
            }

            return View(movieGenre);
        }

        // POST: MovieGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieGenre = await _context.MovieGenre.FindAsync(id);
            _context.MovieGenre.Remove(movieGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieGenreExists(int id)
        {
            return _context.MovieGenre.Any(e => e.MovieId == id);
        }


        public async Task<IActionResult> SearchbyGenre ( string genre )
            {
            var genres = _context.MovieGenre.Include( mg => mg.Genre ).Include( mg => mg.Movie ).ToList().GroupBy( mg => mg.Genre.Type ); // Returns Enumerable with KEY:Genre type VALUE: list of Movies
            var moviesList = genres.First( g => g.Key == genre ); // Filter the list of genres to the right one
            return View( "Index" , moviesList.ToList() );
            }


        }
}
