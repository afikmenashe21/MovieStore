using System;
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
    public class MovieActorsController : Controller
        {
        private readonly MovieStoreContext _context;

        public MovieActorsController ( MovieStoreContext context )
            {
            _context = context;
            }

        // GET: MovieActors
        public async Task<IActionResult> Index ( )
            {
            var movieStoreContext = _context.MovieActor.Include( m => m.Actor ).Include( m => m.Movie );
            return View( await movieStoreContext.ToListAsync() );
            }

        // GET: MovieActors/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movieActor = await _context.MovieActor
                .Include( m => m.Actor )
                .Include( m => m.Movie )
                .FirstOrDefaultAsync( m => m.MovieId == id );
            if ( movieActor == null )
                {
                return NotFound();
                }

            return View( movieActor );
            }

        // GET: MovieActors/Create
        public IActionResult Create ( )
            {
            ViewData [ "ActorId" ] = new SelectList( _context.Actor , "Id" , "Id" );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" );
            return View();
            }

        // POST: MovieActors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( [Bind( "MovieId,ActorId" )] MovieActor movieActor )
            {
            if ( ModelState.IsValid )
                {
                _context.Add( movieActor );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
                }
            ViewData [ "ActorId" ] = new SelectList( _context.Actor , "Id" , "Id" , movieActor.ActorId );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" , movieActor.MovieId );
            return View( movieActor );
            }

        // GET: MovieActors/Edit/5
        public async Task<IActionResult> Edit ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movieActor = await _context.MovieActor.FindAsync( id );
            if ( movieActor == null )
                {
                return NotFound();
                }
            ViewData [ "ActorId" ] = new SelectList( _context.Actor , "Id" , "Id" , movieActor.ActorId );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" , movieActor.MovieId );
            return View( movieActor );
            }

        // POST: MovieActors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "MovieId,ActorId" )] MovieActor movieActor )
            {
            if ( id != movieActor.MovieId )
                {
                return NotFound();
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( movieActor );
                    await _context.SaveChangesAsync();
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !MovieActorExists( movieActor.MovieId ) )
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
            ViewData [ "ActorId" ] = new SelectList( _context.Actor , "Id" , "Id" , movieActor.ActorId );
            ViewData [ "MovieId" ] = new SelectList( _context.Movie , "Id" , "Name" , movieActor.MovieId );
            return View( movieActor );
            }

        // GET: MovieActors/Delete/5
        public async Task<IActionResult> Delete ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movieActor = await _context.MovieActor
                .Include( m => m.Actor )
                .Include( m => m.Movie )
                .FirstOrDefaultAsync( m => m.MovieId == id );
            if ( movieActor == null )
                {
                return NotFound();
                }

            return View( movieActor );
            }

        // POST: MovieActors/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var movieActor = await _context.MovieActor.FindAsync( id );
            _context.MovieActor.Remove( movieActor );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
            }

        private bool MovieActorExists ( int id )
            {
            return _context.MovieActor.Any( e => e.MovieId == id );
            }

        public Dictionary<string , List<string>> MultiselectMovie ( string movie ) // return Dictionary with 2 keys:1.all actors ,2.actors applied for specific movie
            {
            var actorsNames = new List<string>();
            if ( movie != null )
                {
                var movies = _context.MovieActor.Include( ma => ma.Actor ).Include( ma => ma.Movie ).ToList().GroupBy( mg => mg.Movie.Name ); // Returns Enumerable with KEY:Movie name VALUE: list of Actors
                if ( movies.Any( g => g.Key != movie ) )
                    actorsNames = movies.FirstOrDefault( g => g.Key != movie ).Select( v => v.Actor.Name ).ToList(); // Filter the list of movies to the right one and get only the Actors names
                }
            var actorsList = _context.Actor.Select( v => v.Name ).ToList(); //Get all the acors
            var dictionaryData = new Dictionary<string , List<string>>();
            dictionaryData.Add( "data" , actorsList );
            dictionaryData.Add( "checked" , actorsNames );
            return dictionaryData;
            }

        public Dictionary<string , List<string>> MultiselectActor ( string actor ) // return Dictionary with 2 keys:1.all actors ,2.actors applied for specific movie
            {
            var moviesNames = new List<string>();
            if ( actor != null )
                {
                var movies = _context.MovieActor.Include( ma => ma.Actor ).Include( ma => ma.Movie ).ToList().GroupBy( mg => mg.Actor.Name ); // Returns Enumerable with KEY:Movie name VALUE: list of Actors
                if ( movies.Any( g => g.Key == actor ) )
                    moviesNames = movies.FirstOrDefault( g => g.Key == actor ).Select( v => v.Movie.Name ).ToList(); // Filter the list of movies to the right one and get only the Actors names
                }
            var moviesList = _context.Movie.Select( v => v.Name ).ToList(); //Get all the acors
            var dictionaryData = new Dictionary<string , List<string>>();
            dictionaryData.Add( "data" , moviesList );
            dictionaryData.Add( "checked" , moviesNames );
            return dictionaryData;
            }

        }
    }
