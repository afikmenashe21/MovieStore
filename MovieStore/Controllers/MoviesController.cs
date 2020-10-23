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
using System.IO;
using System.Reflection;
using System.Threading;


using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
/*
API Key: 9fde2d96ac6101edcaf57252ac55719d
An example request looks like: https://api.themoviedb.org/3/movie/550?api_key=9fde2d96ac6101edcaf57252ac55719d
*/

/*
 YouTube API key : AIzaSyAEDTLnlZLlF6aK076D2l9VSlFPzyO1QaQ
 */

namespace MovieStore.Controllers
    {
    public class MoviesController : Controller
        {
        //private Boolean exist = false;
        private readonly MovieStoreContext _context;
        //private string accsessKey = "79a6c068";
        string [ ] headlines = { "Great Movie" , "Wowwww" , "Amazing story line" , "I'm shocked" , "Great cast !!" , "MUST TO WATCH" , "I'm impressed" , "A Big Like" };
        Random rnd = new Random();
        List<Movie> movies = new List<Movie>();
        DateTime minDateTime = DateTime.MinValue;
        DateTime maxDateTime = DateTime.MaxValue;

        string youtubeTrailer = "";


        public MoviesController ( MovieStoreContext context )
            {
            _context = context;
            }
        public async Task<IActionResult> HomePage ( )
            {
            string user = HttpContext.Session.GetString( "Type" ); //Function to verify the user before get in the view
                                                                   //if ( user == null )
                                                                   //    return RedirectToAction( "Login" , "Users" );
                                                                   //else
            var movielist = await _context.Movie.ToListAsync();
            movielist.Reverse();
            TrimGenrelist(); // Trim the list of Genres to 3 columns
            return View( movielist.Take( 5 ) ); // Returns the last 5 movies entered the database
            }
        public async Task<IActionResult> Search ( string name )
            {
            movies.Clear();
            //foreach (Movie m in _context.Movie)
            //{
            //    await Run(m.Name);
            //    m.Trailer = youtubeTrailer;
            //}

            //Interpreted user's search target

            string [ ] title = name.Split( " " );
            string resultTitle = null;
            foreach ( string s in title )
                {
                resultTitle += s + "+";
                }

            Movie movie = new Movie();
            await CreateMovie( movie , resultTitle );//Creates reviews either

            return RedirectToAction( "Details" , movies [ 0 ] );
            }

        // GET: Movies
        public async Task<IActionResult> Index ( )
            {
            return View( await _context.Movie.OrderBy(m=>m.Name).ToListAsync() );
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

            ViewBag.reviews = await _context.Review.Where( r => r.Movie.Id == id ).Include( r => r.Author ).ToListAsync();
            ViewBag.reviews.Reverse();
            // Linq - first filter the rows in MovieGenre and then join to get the Genres

            ViewBag.genres = await _context.MovieGenre.Where( mg => mg.MovieId == id ).Join( _context.Genre , mg => mg.GenreId , g => g.Id , ( mg , g ) => g ).ToListAsync();

            // Linq - first filter the rows in MovieActor and then join to get the Actors
            ViewBag.actors = await _context.MovieActor.Where( ma => ma.MovieId == id ).Join( _context.Actor , ma => ma.ActorId , a => a.Id , ( ma , a ) => a ).ToListAsync();

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
        public async Task<IActionResult> Create ( [Bind( "Id,Name,ReleaseDate,Duration,Director,Poster,Trailer,Storyline,AverageRating" )] Movie movie )
            {
            if ( ModelState.IsValid )
                {
                _context.Add( movie );
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

        private bool MovieExists ( int id )
            {
            return _context.Movie.Any( e => e.Id == id );
            }

        public async Task<IActionResult> DynamicSearch ( string term )
            {
            var query = from m in _context.Movie
                        where m.Name.Contains( term )
                        select new { id = m.Id , label = m.Name };

            return Json( await query.ToListAsync() );
            }

        /*---------------Custom Methods---------------*/

        private async Task CreateMovieReviews ( Movie movie )
            {
            string imdbID = movie.imdbID;
            var baseUrl = "https://api.themoviedb.org/3/movie/" + imdbID + "/reviews?api_key=9fde2d96ac6101edcaf57252ac55719d&language=en-US&page=1";

            //Have your using statements within a try/catch block
            try
                {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using ( HttpClient client = new HttpClient() )
                    {
                    //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using ( HttpResponseMessage res = await client.GetAsync( baseUrl ) )
                        {
                        //Then get the content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using ( HttpContent content = res.Content )
                            {
                            //Now assign your content to your data variable, by converting into a string using the await keyword.
                            var data = await content.ReadAsStringAsync();
                            //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                            if ( data != null )
                                {
                                JObject jObj = JObject.Parse( data );
                                int numOfReviews = 5;//Int32.Parse(jObj["total_results"].ToString());
                                var allReviews = jObj [ "results" ];
                                if ( allReviews.Count() != 0 )
                                    {
                                    for ( int i = 0 ; i < numOfReviews ; i++ )
                                        {
                                        var reviews = allReviews [ i ];
                                        Review review = new Review();
                                        review.Content = reviews [ "content" ].ToString();
                                        review.Rating = Math.Round( ( rnd.NextDouble() * ( 10.0 - 0.0 ) ) , 1 );
                                        review.Headline = headlines [ rnd.Next( 0 , 7 ) ];
                                        DateTime start = new DateTime( 1995 , 1 , 1 );
                                        int range = ( DateTime.Today - start ).Days;
                                        review.Published = start.AddDays( rnd.Next( range ) );
                                        review.Movie = (Movie) _context.Movie.FirstOrDefault( m => m.imdbID == imdbID );
                                        // Todo - fix it to generate a number from a list which should add ids when new user is created
                                        int userID = rnd.Next( 1 , 11 );
                                        review.Author = (User) _context.User.FirstOrDefault( u => u.Id == userID );

                                        if ( review.Movie.Comments == null )
                                            {
                                            review.Movie.Comments = new List<Review>();
                                            }
                                        if ( review.Author.Comments == null )
                                            {
                                            review.Author.Comments = new List<Review>();
                                            }
                                        review.Movie.Comments.Add( review );
                                        review.Author.Comments.Add( review );
                                        _context.Add( review );
                                        await _context.SaveChangesAsync();

                                        }
                                    }

                                }
                            else
                                {
                                Console.WriteLine( "NO Data----------" );
                                }
                            }
                        }
                    }
                }
            catch ( Exception exception )
                {
                Console.WriteLine( "Exception Hit------------" );
                Console.WriteLine( exception );
                }

            }

        private async Task CreateMovie ( Movie movie , string resultTitle )
            {
            //Define your baseUrl
            var baseUrl = "http://www.omdbapi.com/" + "?apikey=79a6c068&t=" + resultTitle.Remove( resultTitle.Length - 1 );

            //Have your using statements within a try/catch block
            try
                {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using ( HttpClient client = new HttpClient() )
                    {
                    //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using ( HttpResponseMessage res = await client.GetAsync( baseUrl ) )
                        {
                        //Then get the content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using ( HttpContent content = res.Content )
                            {
                            //Now assign your content to your data variable, by converting into a string using the await keyword.
                            var data = await content.ReadAsStringAsync();
                            //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                            if ( data != null )
                                {
                                JObject jObj = JObject.Parse( data );
                                string theMovieName = jObj [ "Title" ].ToString();
                                string imdbID = jObj [ "imdbID" ].ToString();
                                IEnumerable<Movie> movieList = new List<Movie>();
                                var query = _context.Movie.Where( s => s.Name == theMovieName ).FirstOrDefault<Movie>();

                                if ( query == null ) //The movie is not in the database
                                    {
                                    movie.Name = theMovieName;
                                    movie.ReleaseDate = DateTime.Parse( jObj [ "Released" ].ToString() );
                                    movie.Duration = Int32.Parse( ( jObj [ "Runtime" ].ToString().Split( " " ) ) [ 0 ] );
                                    movie.Director = jObj [ "Director" ].ToString();
                                    movie.Poster = jObj [ "Poster" ].ToString();
                                    await Run( theMovieName );
                                    movie.Trailer = youtubeTrailer;
                                    movie.Storyline = jObj [ "Plot" ].ToString();
                                    movie.AverageRating = Double.Parse( jObj [ "imdbRating" ].ToString() );
                                    string [ ] genres = jObj [ "Genre" ].ToString().Split( ", " ).ToArray();
                                    movie.imdbID = jObj [ "imdbID" ].ToString();

                                    foreach ( string genreName in genres )
                                        {
                                        var genreFromTable = _context.Genre.Where( s => s.Type == genreName ).FirstOrDefault<Genre>();

                                        var genre = new Genre();

                                        if ( genreFromTable != null )//exist in db
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

                                        if ( genre.MovieGenre == null )
                                            {
                                            genre.MovieGenre = new List<MovieGenre>();
                                            }
                                        if ( movie.MovieGenre == null )
                                            {
                                            movie.MovieGenre = new List<MovieGenre>();
                                            }

                                        genre.MovieGenre.Add( movieGenre );
                                        movie.MovieGenre.Add( movieGenre );

                                        }

                                    string [ ] actorsNames = jObj [ "Actors" ].ToString().Split( ", " ).ToArray();

                                    foreach ( string actorName in actorsNames )
                                        {
                                        var actorFromTable = _context.Actor.Where( s => s.Name == actorName ).FirstOrDefault<Actor>();

                                        var actor = new Actor();

                                        if ( actorFromTable != null )//exist in db
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

                                        if ( actor.MovieActor == null )
                                            {
                                            actor.MovieActor = new List<MovieActor>();
                                            }
                                        if ( movie.MovieActor == null )
                                            {
                                            movie.MovieActor = new List<MovieActor>();
                                            }
                                        actor.MovieActor.Add( movieActor );
                                        movie.MovieActor.Add( movieActor );


                                        }
                                    await this.Create( movie );
                                    await CreateMovieReviews( movie );
                                    movies.Add( movie );
                                    }
                                else // The movie is in the database 
                                    {
                                    //exist = true;
                                    movie = _context.Movie.FirstOrDefault( p => p.Name.Contains( theMovieName ) );
                                    movies.Add( movie );

                                    }

                                }
                            else
                                {
                                Console.WriteLine( "NO Data----------" );
                                }
                            }
                        }
                    }
                }
            catch ( Exception exception )
                {
                Console.WriteLine( "Exception Hit------------" );
                Console.WriteLine( exception );
                }
            }

        public async Task<IActionResult> AdvancedSearch ( string releasedate = "1980 - 2021" , string duration = "0 - 240" , string rating = "0 - 10" )
            {

            int [ ] fromYearsto = Array.ConvertAll( releasedate.Split( " - " ) , y => int.Parse( y ) ); // Convert Releasedate from string to int array
            int [ ] fromDurationsto = Array.ConvertAll( duration.Split( " - " ) , y => int.Parse( y ) ); // Convert Duration from string to int array
            int [ ] fromRatingto = Array.ConvertAll( rating.Split( " - " ) , y => int.Parse( y ) );// Convert Rating from string to int array

            // Return the movies that qualify these terms
            var query = from m in _context.Movie
                        where m.ReleaseDate.Year >= fromYearsto [ 0 ] && m.ReleaseDate.Year <= fromYearsto [ 1 ]
                        && m.Duration >= fromDurationsto [ 0 ] && m.Duration <= fromDurationsto [ 1 ]
                        && m.AverageRating >= fromRatingto [ 0 ] && m.AverageRating <= fromRatingto [ 1 ]
                        select m;
            return View( "Index" , await query.ToListAsync() );
            }

        public async Task Run ( string movieName )
            {

            var youtubeService = new YouTubeService( new BaseClientService.Initializer()
                {
                ApiKey = "AIzaSyAEDTLnlZLlF6aK076D2l9VSlFPzyO1QaQ" ,
                ApplicationName = this.GetType().ToString()
                } );

            var searchListRequest = youtubeService.Search.List( "snippet" );
            searchListRequest.Q = movieName + " trailer";
            searchListRequest.MaxResults = 10;
            searchListRequest.Type = "video";
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            youtubeTrailer = "https://www.youtube.com/embed/" + searchListResponse.Items [ 0 ].Id.VideoId;


            }

        private void TrimGenrelist ( )
            {
            var genres = _context.Genre.Select( g => g.Type ).ToList();
            int amount = genres.Count;
            if ( amount > 2 ) // If amount of genres more then/equal 3 
                {
                int range = (int) Math.Floor( (double) ( amount / 3 ) );
                switch ( amount % 3 )
                    {
                    case 0: // If amount of genres divide by 3
                        ViewBag.firstcolumn = genres.GetRange( 0 , range );
                        ViewBag.secondcolumn = genres.GetRange( range , range );
                        ViewBag.thirdcolumn = genres.GetRange( 2 * ( range ) , range );
                        break;
                    case 1: // If amount of genres divide by 3 with 1 remainder
                        ViewBag.firstcolumn = genres.GetRange( 0 , range + 1 ); // This column will have 1 more genre
                        ViewBag.secondcolumn = genres.GetRange( range + 1 , range );
                        ViewBag.thirdcolumn = genres.GetRange( 2 * ( range ) + 1 , range );
                        break;
                    case 2: // If amount of genres divide by 3 with 2 remainder
                        ViewBag.firstcolumn = genres.GetRange( 0 , range + 1 ); // This column will have 1 more genre
                        ViewBag.secondcolumn = genres.GetRange( range + 1 , range + 1 ); // This column will have 1 more genre
                        ViewBag.thirdcolumn = genres.GetRange( 2 * ( range ) + 2 , range );
                        break;
                    }
                }
            else // If amount of genres less then 3
                {
                ViewBag.firstcolumn = genres.GetRange( 0 , 1 );
                if ( amount > 1 ) // If amount of genres equal to 2
                    ViewBag.secondcolumn = genres.GetRange( 1 , 1 );
                }

            }
        }
    }