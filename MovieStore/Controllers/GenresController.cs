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
    public class GenresController : Controller
        {
        private readonly MovieStoreContext _context;

        public GenresController ( MovieStoreContext context )
            {
            _context = context;
            }

        // GET: Genres
        public async Task<IActionResult> Index ( )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            return View( await _context.Genre.ToListAsync() );
            }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( id == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            var genre = await _context.Genre
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( genre == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            return View( genre );
            }

        // GET: Genres/Create
        public IActionResult Create ( )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            return View();
            }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( [Bind( "Id,Type,MovieId" )] Genre genre )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( ModelState.IsValid )
                {
                _context.Add( genre );
                await _context.SaveChangesAsync();
                if ( movies != null ) // If any movie is added/removed
                    EditMovies( movies , genre.Id ); // Add or remove the selected Movie
                return RedirectToAction( nameof( Index ) );
                }
            return View( genre );
            }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit ( int? id )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( id == null )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            var genre = await _context.Genre.FindAsync( id );
            if ( genre == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }
            return View( genre );
            }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,Type,MovieId" )] Genre genre )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( id != genre.Id )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( genre );
                    await _context.SaveChangesAsync();
                    if ( movies != null ) // If any movie is added/removed
                        EditMovies( movies , genre.Id ); // Add or remove the selected Movie
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !GenreExists( genre.Id ) )
                        {
                        return NotFound();
                        }
                    else
                        {
                        throw;
                        }
                    }
                return RedirectToAction( "Dashboard" , "Users" );
                }
            return View( genre );
            }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete ( int? id )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( id == null )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            var genre = await _context.Genre
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( genre == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            return View( genre );
            }

        // POST: Genres/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            var genre = await _context.Genre.FindAsync( id );
            _context.Genre.Remove( genre );
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var genre = await _context.Genre.Include( g => g.MovieGenre ).Include(g=>g.UserGenre).Where( g => g.Id == id ).FirstOrDefaultAsync();
            foreach ( var movieGenre in genre.MovieGenre )
                {
                var movie = await _context.Movie.Include( g => g.MovieGenre ).Where( m => m.Id == movieGenre.MovieId ).FirstOrDefaultAsync();
                movie.MovieGenre.Remove( movieGenre );
                _context.MovieGenre.Remove( movieGenre );
                }
            foreach ( var userGenre in genre.UserGenre )
                {
                var user = await _context.User.Include( u => u.UserGenre ).Where( m => m.Id == userGenre.UserId ).FirstOrDefaultAsync();
                user.UserGenre.Remove( userGenre );
                _context.UserGenre.Remove( userGenre );
                }
            _context.Genre.Remove( genre );
            await _context.SaveChangesAsync();
            return RedirectToAction( "Dashboard" , "Users" );
            }

        private bool GenreExists ( int id )
            {
            return _context.Genre.Any( e => e.Id == id );
            }

        public async Task<Dictionary<string , int>> Graph ( ) // return dic -> key:genre name , value: counter
            {
            var queryList = await _context.MovieGenre.Include( mg => mg.Genre ).ToListAsync();
            var queryMap = queryList.GroupBy( q => q.Genre.Type ).ToDictionary( k => k.Key , v => v.Count() );
            return queryMap;
            }

        public void EditMovies ( string movies , int genreId ) // ADD or REMOVE movies from genres
            {
            var oldMovies = _context.MovieGenre.Include( mg => mg.Movie ).Where( mg => mg.GenreId == genreId ).ToList(); // Get the genres related to movie
            var genre = _context.Genre.Include( m => m.MovieGenre ).Where( m => m.Id == genreId ).FirstOrDefault();
            var newMovies = movies.Split( "," ); // Split the selected genres to array
            foreach ( string movie in newMovies ) // Check if there is any new genres to add the movie
                {
                if ( !oldMovies.Any( mg => mg.Movie.Name == movie ) ) // Check if the selected genres already connected to the movie
                    {
                    var newMovie = _context.Movie.Include( g => g.MovieGenre ).Where( m => m.Name == movie ).FirstOrDefault(); // Find the missing genre and add to a list
                    MovieGenre movieGenre = new MovieGenre()
                        {
                        MovieId = newMovie.Id ,
                        Movie = newMovie ,
                        GenreId = genre.Id ,
                        Genre = genre ,
                        };
                    if ( newMovie.MovieGenre == null )
                        newMovie.MovieGenre = new List<MovieGenre>();
                    newMovie.MovieGenre.Add( movieGenre );
                    if ( genre.MovieGenre == null )
                        genre.MovieGenre = new List<MovieGenre>();
                    genre.MovieGenre.Add( movieGenre );
                    _context.Add( movieGenre );
                    }
                }
            foreach ( MovieGenre movie in oldMovies ) // Check if there is any old genres to removed from the movie genres
                {
                if ( !newMovies.Any( mg => mg == movie.Movie.Name ) )
                    {
                    var removedMovie = _context.Movie.Include( g => g.MovieGenre ).Where( g => g.Name == movie.Movie.Name ).FirstOrDefault(); // Find the missing actor and add to a list
                    removedMovie.MovieGenre.Remove( movie );
                    genre.MovieGenre.Remove( movie );
                    _context.MovieGenre.Remove( movie );
                    }
                }
            _context.SaveChanges();
            }
        }
    }
