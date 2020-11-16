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
    public class ActorsController : Controller
        {
        private readonly MovieStoreContext _context;

        public ActorsController ( MovieStoreContext context )
            {
            _context = context;
            }

        // GET: Actors
        public async Task<IActionResult> Index ( )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            return View( await _context.Actor.ToListAsync() );
            }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details ( int? id )
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

            var actor = await _context.Actor
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( actor == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            return View( actor );
            }

        // GET: Actors/Create
        public IActionResult Create ( )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            return View();
            }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( [Bind( "Id,Name" )] Actor actor , string movies = null )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( _context.Actor.Any( a => a.Name == actor.Name ) )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }
            if ( ModelState.IsValid )
                {
                _context.Add( actor );
                await _context.SaveChangesAsync();
                if ( movies != null ) // If any movie is added/removed
                    EditMovies( movies , actor.Id ); // Add or remove the selected Movie
                return RedirectToAction( "Dashboard" , "Users" );
                }
            return View( actor );
            }

        // GET: Actors/Edit/5
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
            var actor = await _context.Actor.FindAsync( id );
            if ( actor == null )
                {
                ViewBag.errr = 404;
                return View( "ClientError" );
                }
            TempData [ "returnURL" ] = HttpContext.Request.Headers [ "Referer" ].ToString(); // Save the last page viewed to be able to return back to him
            return View( actor );
            }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,Name" )] Actor actor , string movies )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( id != actor.Id )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( actor );
                    await _context.SaveChangesAsync();
                    if ( movies != null ) // If any movie is added/removed
                        EditMovies( movies , actor.Id ); // Add or remove the selected Movie
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !ActorExists( actor.Id ) )
                        {
                        ViewBag.error = 404;
                        return View( "ClientError" );
                        }
                    else
                        {
                        throw;
                        }
                    }
                return Redirect( TempData [ "returnURL" ].ToString() ); // return to Move deatils
                }
            return View( actor );
            }

        // GET: Actors/Delete/5
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

            var actor = await _context.Actor
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( actor == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            return View( actor );
            }

        // POST: Actors/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            var actor = await _context.Actor.Include( a => a.MovieActor ).Where( a => a.Id == id ).FirstOrDefaultAsync();
            foreach ( var movieActor in actor.MovieActor )
                {
                var movie = await _context.Movie.Include( g => g.MovieActor ).Where( m => m.Id == movieActor.MovieId ).FirstOrDefaultAsync();
                movie.MovieActor.Remove( movieActor );
                _context.MovieActor.Remove( movieActor );
                }
            _context.Actor.Remove( actor );
            await _context.SaveChangesAsync();
            return RedirectToAction( "Dashboard" , "Users" );
            }

        private bool ActorExists ( int id )
            {
            return _context.Actor.Any( e => e.Id == id );
            }

        public void EditMovies ( string movies , int actorId ) // ADD or REMOVE movies from actor
            {
            var oldMovies = _context.MovieActor.Include( ma => ma.Movie ).Where( ma => ma.ActorId == actorId ).ToList(); // Get the actors related to movie
            var actor = _context.Actor.Include( m => m.MovieActor ).Where( a => a.Id == actorId ).FirstOrDefault();
            var newMovies = movies.Split( "," ); // Split the selected movies to array
            foreach ( string movie in newMovies ) // Check if there is any new movies to add the actor
                {
                if ( !oldMovies.Any( mg => mg.Movie.Name == movie ) ) // Check if the selected actors already connected to the movie
                    {
                    var newMovie = _context.Movie.Include( g => g.MovieActor ).Where( m => m.Name == movie ).FirstOrDefault(); // Find the missing actor and add to a list
                    MovieActor movieGenre = new MovieActor()
                        {
                        MovieId = newMovie.Id ,
                        Movie = newMovie ,
                        ActorId = actor.Id ,
                        Actor = actor ,
                        };
                    if ( newMovie.MovieActor == null )
                        newMovie.MovieActor = new List<MovieActor>();
                    newMovie.MovieActor.Add( movieGenre );
                    if ( actor.MovieActor == null )
                        actor.MovieActor = new List<MovieActor>();
                    actor.MovieActor.Add( movieGenre );
                    _context.Add( movieGenre );
                    }
                }
            foreach ( MovieActor movie in oldMovies ) // Check if there is any old actors to removed from the movie actors
                {
                if ( !newMovies.Any( mg => mg == movie.Movie.Name ) )
                    {
                    var removedMovie = _context.Movie.Include( g => g.MovieActor ).Where( g => g.Name == movie.Movie.Name ).FirstOrDefault(); // Find the missing actor and add to a list
                    removedMovie.MovieActor.Remove( movie );
                    actor.MovieActor.Remove( movie );
                    _context.MovieActor.Remove( movie );
                    }
                }
            _context.SaveChanges();
            }
        }
    }
