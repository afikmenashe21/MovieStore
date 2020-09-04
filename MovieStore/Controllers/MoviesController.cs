using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MovieStore.Data;
using MovieStore.Models;
using Newtonsoft.Json.Linq;

/*
 API Key: 9fde2d96ac6101edcaf57252ac55719d
An example request looks like: https://api.themoviedb.org/3/movie/550?api_key=9fde2d96ac6101edcaf57252ac55719d
 */

namespace MovieStore.Controllers
    {
    public class MoviesController : Controller
        {

        private readonly MovieStoreContext _context;
        private string accsessKey = "79a6c068";

        public MoviesController ( MovieStoreContext context )
            {
            _context = context;
        }


            //Define your baseUrl
            var baseUrl = "http://www.omdbapi.com/" + "?apikey=79a6c068&t=" + resultTitle.Remove(resultTitle.Length - 1);

            //Have your using statements within a try/catch block
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        //Then get the content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using (HttpContent content = res.Content)
                        {
                            //Now assign your content to your data variable, by converting into a string using the await keyword.
                            var data = await content.ReadAsStringAsync();
                            //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                            if (data != null)
                            {
                                JObject jObj = JObject.Parse(data);
                                string theMovieName = jObj["Title"].ToString();
                                string imdbID = jObj["imdbID"].ToString();
                                Movie movie = new Movie();
                                IEnumerable<Movie> movieList = new List<Movie>();
                                var query = _context.Movie.Where(s => s.Name == theMovieName).FirstOrDefault<Movie>();

                                if (query == null) //The movie is not in the database
                                {
                                    createMovie(movie, theMovieName, jObj);
                                    await this.Create(movie);
                                }
                                else // The movie is in the database 
                                {
                                    movie = (Movie)_context.Movie.Where(p => p.Name.Contains(theMovieName));
                                }


                                return View("Index", movieList.Append(movie));

                            }
                            else
                            {
                                Console.WriteLine("NO Data----------");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }
            return View("Index", null);
        }

        private void createMovie(Movie movie, string theMovieName, JObject jObj)
        {
            Random rnd = new Random();
            movie.Name = theMovieName;
            movie.ReleaseDate = DateTime.Parse(jObj["Released"].ToString());
            movie.Duration = Int32.Parse((jObj["Runtime"].ToString().Split(" "))[0]);
            movie.Director = jObj["Director"].ToString();
            movie.Poster = jObj["Poster"].ToString();
            movie.Trailer = "No Trailer"; // Figure out how to get the movie trailer
            movie.Storyline = jObj["Plot"].ToString();
            movie.AverageRating = Double.Parse(jObj["imdbRating"].ToString());
            string[] genres = jObj["Genre"].ToString().Split(", ").ToArray();
            movie.imdbID = jObj["imdbID"].ToString();

            foreach (string genreName in genres)
            {
                var genreFromTable = _context.Genre.Where(s => s.Type == genreName).FirstOrDefault<Genre>();

                var genre = new Genre();

                if (genreFromTable != null)//exist in db
                {
                    genre = genreFromTable;
                }
                else // The actor in not the database table
                {
                    genre.Type = genreName;
                }

                MovieGenre movieGenre = new MovieGenre();
                movieGenre.Movie = movie;
                movieGenre.Genre = genre;

                if (genre.MovieGenre == null)
                {
                    genre.MovieGenre = new List<MovieGenre>();
                }
                if (movie.MovieGenre == null)
                {
                    movie.MovieGenre = new List<MovieGenre>();
                }

                genre.MovieGenre.Add(movieGenre);
                movie.MovieGenre.Add(movieGenre);

            }

            string[] actorsNames = jObj["Actors"].ToString().Split(", ").ToArray();

            foreach (string actorName in actorsNames)
            {
                var actorFromTable = _context.Actor.Where(s => s.Name == actorName).FirstOrDefault<Actor>();

                var actor = new Actor();

                if (actorFromTable != null)//exist in db
                {
                    actor = actorFromTable;
                }
                else // The actor in not the database table
                {
                    actor.Name = actorName;
                }

                MovieActor movieActor = new MovieActor();
                movieActor.Movie = movie;
                movieActor.Actor = actor;

                if (actor.MovieActor == null)
                {
                    actor.MovieActor = new List<MovieActor>();
                }
                if (movie.MovieActor == null)
                {
                    movie.MovieActor = new List<MovieActor>();
                }
                actor.MovieActor.Add(movieActor);
                movie.MovieActor.Add(movieActor);


            }

        }


        // GET: Movies
        public async Task<IActionResult> Index ( )
            {
            string user = HttpContext.Session.GetString( "Type" ); //Function to verify the user before get in the view
            if ( user == null )
                return RedirectToAction( "Login" , "Users" );
            else
                return View( await _context.Movie.ToListAsync() );
            }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movie = await _context.Movie
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( movie == null )
                {
                return NotFound();
                }

            return View( movie );
            }

        // GET: Movies/Create
        public IActionResult Create ( )
            {
            return View();
            }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,Duration,Director,Poster,Trailer,Storyline,AverageRating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
                }
            return View( movie );
            }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movie = await _context.Movie.FindAsync( id );
            if ( movie == null )
                {
                return NotFound();
                }
            return View( movie );
            }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,Name,ReleaseDate,Duration,Director,Poster,Trailer,Storyline,AverageRating" )] Movie movie )
            {
            if ( id != movie.Id )
                {
                return NotFound();
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( movie );
                    await _context.SaveChangesAsync();
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !MovieExists( movie.Id ) )
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
            return View( movie );
            }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var movie = await _context.Movie
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( movie == null )
                {
                return NotFound();
                }

            return View( movie );
            }

        // POST: Movies/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var movie = await _context.Movie.FindAsync( id );
            _context.Movie.Remove( movie );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
            }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
