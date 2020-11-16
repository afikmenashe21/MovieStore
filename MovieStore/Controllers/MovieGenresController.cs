using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public MovieGenresController ( MovieStoreContext context )
            {
            _context = context;
            }

        // GET: MovieGenres
        public async Task<IActionResult> Index ( )
            {
            var movieStoreContext = _context.MovieGenre.Include( m => m.Genre ).Include( m => m.Movie );
            return View( await movieStoreContext.ToListAsync() );
            }

        // GET: MovieGenres/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movieGenre = await _context.MovieGenre
                .Include( m => m.Genre )
                .Include( m => m.Movie )
                .FirstOrDefaultAsync( m => m.MovieId == id );
            if ( movieGenre == null )
                {
                return NotFound();
                }

            return View( movieGenre );
            }

        // GET: MovieGenres/Create
        public IActionResult Create ( )
            {
            ViewData [ "GenreId" ] = new SelectList( _context.Genre , "Id" , "Type" );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" );
            return View();
            }

        // POST: MovieGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( [Bind( "MovieId,GenreId" )] MovieGenre movieGenre )
            {
            if ( ModelState.IsValid )
                {
                _context.Add( movieGenre );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
                }
            ViewData [ "GenreId" ] = new SelectList( _context.Genre , "Id" , "Type" , movieGenre.GenreId );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" , movieGenre.MovieId );
            return View( movieGenre );
            }

        // GET: MovieGenres/Edit/5
        public async Task<IActionResult> Edit ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movieGenre = await _context.MovieGenre.FindAsync( id );
            if ( movieGenre == null )
                {
                return NotFound();
                }
            ViewData [ "GenreId" ] = new SelectList( _context.Genre , "Id" , "Type" , movieGenre.GenreId );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" , movieGenre.MovieId );
            return View( movieGenre );
            }

        // POST: MovieGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "MovieId,GenreId" )] MovieGenre movieGenre )
            {
            if ( id != movieGenre.MovieId )
                {
                return NotFound();
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( movieGenre );
                    await _context.SaveChangesAsync();
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !MovieGenreExists( movieGenre.MovieId ) )
                        {
                        return NotFound();
                        }
                    else
                        {
                        throw;
                        }
                    }
                return RedirectToAction( nameof( Index ) );
                }
            ViewData [ "GenreId" ] = new SelectList( _context.Genre , "Id" , "Type" , movieGenre.GenreId );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" , movieGenre.MovieId );
            return View( movieGenre );
            }

        // GET: MovieGenres/Delete/5
        public async Task<IActionResult> Delete ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movieGenre = await _context.MovieGenre
                .Include( m => m.Genre )
                .Include( m => m.Movie )
                .FirstOrDefaultAsync( m => m.MovieId == id );
            if ( movieGenre == null )
                {
                return NotFound();
                }

            return View( movieGenre );
            }

        // POST: MovieGenres/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var movieGenre = await _context.MovieGenre.FindAsync( id );
            _context.MovieGenre.Remove( movieGenre );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
            }

        private bool MovieGenreExists ( int id )
            {
            return _context.MovieGenre.Any( e => e.MovieId == id );
            }


        public IActionResult SearchbyGenre ( string genre )
            {
            if ( genre == null )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }
            var genres = _context.MovieGenre.Include( mg => mg.Genre ).Include( mg => mg.Movie ).ToList().GroupBy( mg => mg.Genre.Type ); // Returns Enumerable with KEY:Genre type VALUE: list of Movies
            var moviesList = genres.FirstOrDefault( g => g.Key == genre ); // Filter the list of genres to the right one
            return View( "Index" , moviesList.ToList() );
            }

        public Dictionary<string , List<string>> MultiselectMovie ( string movie ) // return Dictionary with 2 keys:1.all genres,2.genres applied for specific movie
            {
            var genresNames = new List<string>();
            if ( movie != null )
                {
                var movies = _context.MovieGenre.Include( mg => mg.Genre ).Include( mg => mg.Movie ).ToList().GroupBy( mg => mg.Movie.Name ); // Returns Enumerable with KEY:Movie name VALUE: list of Genres
                if ( movies.Any( g => g.Key == movie ) )
                    genresNames = movies.FirstOrDefault( g => g.Key == movie ).Select( v => v.Genre.Type ).ToList(); // Filter the list of movies to the right one and get only the Genres names
                }
            var dictionaryData = new Dictionary<string , List<string>>();
            if ( _context.Genre.Any() )
                {
                var genresList = _context.Genre.Select( v => v.Type ).ToList(); //Get all the genres
                dictionaryData.Add( "data" , genresList );
                }
            dictionaryData.Add( "checked" , genresNames );
            return dictionaryData;
            }

        public Dictionary<string , List<string>> MultiselectGenre ( string genre ) // return Dictionary with 2 keys:1.all movies,2.movies applied for specific movie
            {
            var moviesNames = new List<string>();
            if ( genre != null )
                {
                var genres = _context.MovieGenre.Include( mg => mg.Genre ).Include( mg => mg.Movie ).ToList().GroupBy( mg => mg.Genre.Type ); // Returns Enumerable with KEY:Genre name VALUE: list of Movies
                if ( genres.Any( g => g.Key == genre ) )
                    moviesNames = genres.FirstOrDefault( g => g.Key == genre ).Select( v => v.Movie.Name ).ToList(); // Filter the list of Genres to the right one and get only the Movies names                }
                }
            var dictionaryData = new Dictionary<string , List<string>>();
            if ( _context.Movie.Any() )
                {
                var moviesList = _context.Movie.Select( v => v.Name ).ToList(); //Get all the movies
                dictionaryData.Add( "data" , moviesList );
                }
            dictionaryData.Add( "checked" , moviesNames );
            return dictionaryData;
            }

        }
    }
