﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Models;
using Newtonsoft.Json.Linq;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MovieStore.Models.UserPreferences;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
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
        DateTime minDateTime = DateTime.MinValue;
        DateTime maxDateTime = DateTime.MaxValue;

        string youtubeTrailer = "";


        public MoviesController ( MovieStoreContext context )
            {

            _context = context;
            }
        public async Task<IActionResult> HomePage ( )
            {
            string user = HttpContext.Session.GetString( "Type" );
            var movielist = await _context.Movie.ToListAsync();
            movielist.Reverse(); // Reverse the list to get the latest movies added
            await StoreGenresDP(); // store the Genre dropdown list
            return View( movielist.Take( 5 ) ); // Returns the last 5 movies entered the database
            }
        public async Task<IActionResult> Search ( string name )
            {
            if ( name != null )
                {
                //Interpreted user's search target
                string [ ] title = name.Split( " " );
                string resultTitle = null;
                foreach ( string s in title )
                    {
                    resultTitle += s + "+";
                    }

                var movie = await CreateMovie( resultTitle );//Creates reviews either
                if ( movie == null ) // If movie not found
                    return View( "NoResults" );
                return RedirectToAction( "Details" , movie );
                }
            ViewBag.error = 401;
            return View( "ClientError" );
            }

        // GET: Movies
        public async Task<IActionResult> Index ( )
            {
            return View( await _context.Movie.ToListAsync() );
            }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( id == null )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            var movie = await _context.Movie
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( movie == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            ViewBag.reviews = await _context.Review.Where( r => r.Movie.Id == id ).Include( r => r.Author ).OrderByDescending( r => r.Published ).ToListAsync();
            // Linq - first filter the rows in MovieGenre and then join to get the Genres
            ViewBag.genres = await _context.MovieGenre.Where( mg => mg.MovieId == id ).Join( _context.Genre , mg => mg.GenreId , g => g.Id , ( mg , g ) => g ).ToListAsync();
            // Linq - first filter the rows in MovieActor and then join to get the Actors
            ViewBag.actors = await _context.MovieActor.Where( ma => ma.MovieId == id ).Join( _context.Actor , ma => ma.ActorId , a => a.Id , ( ma , a ) => a ).ToListAsync();
            SetgenreSuggestions( ViewBag.genres );
            return View( movie );
            }

        // GET: Movies/Create
        public IActionResult Create ( )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" ) // User not logged or it's not Admin 
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            return View();
            }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( [Bind( "Id,Name,ReleaseDate,Duration,Director,Poster,Trailer,Storyline,AverageRating,imdbID" )] Movie movie , string genres = null , string actors = null )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" ) // User not logged or it's not Admin 
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( ModelState.IsValid )
                {
                _context.Add( movie );
                await _context.SaveChangesAsync();
                if ( genres != null ) // If any genre is added/removed
                    EditGenres( genres , movie.Id ); // Add or remove the selected Genres

                if ( actors != null )// If any actor is added/removed
                    EditActors( actors , movie.Id ); // Add or remove the selected Genres
                return RedirectToAction( "Dashboard" , "Users" );
                }
            return View( movie );
            }

        // GET: Movies/Edit/5
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

            var movie = await _context.Movie.FindAsync( id );
            if ( movie == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }
            TempData [ "returnURL" ] = HttpContext.Request.Headers [ "Referer" ].ToString(); // Save the last page viewed to be able to return back to him
            return View( movie );
            }


        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,Name,ReleaseDate,Duration,Director,Poster,Trailer,Storyline,AverageRating" )] Movie movie , string genres , string actors )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( id != movie.Id )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( movie );
                    await _context.SaveChangesAsync();
                    if ( genres != null ) // If any genre is added/removed
                        EditGenres( genres , movie.Id ); // Add or remove the selected Genres

                    if ( actors != null )// If any actor is added/removed
                        EditActors( actors , movie.Id ); // Add or remove the selected Genres
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !MovieExists( movie.Id ) )
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
            return View( movie );
            }

        // GET: Movies/Delete/5
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

            var movie = await _context.Movie
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( movie == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }
            return View( movie );
            }


        // POST: Movies/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var movie = await _context.Movie.Include( m => m.MovieActor ).Include( m => m.MovieGenre ).Include( m => m.Comments ).Where( m => m.Id == id ).FirstOrDefaultAsync();
            foreach ( var movieActor in movie.MovieActor )
                {
                var actor = await _context.Actor.Include( a => a.MovieActor ).Where( a => a.Id == movieActor.ActorId ).FirstOrDefaultAsync();
                actor.MovieActor.Remove( movieActor );
                _context.MovieActor.Remove( movieActor );
                }
            foreach ( var movieGenre in movie.MovieGenre )
                {
                var genre = await _context.Genre.Include( g => g.MovieGenre ).Where( g => g.Id == movieGenre.GenreId ).FirstOrDefaultAsync();
                genre.MovieGenre.Remove( movieGenre );
                _context.MovieGenre.Remove( movieGenre );
                }
            foreach ( var comment in movie.Comments )
                {
                var review = await _context.Review.Where( r => r.Id == comment.Id ).FirstOrDefaultAsync();
                _context.Review.Remove( review );
                }
            _context.Movie.Remove( movie );
            await _context.SaveChangesAsync();
            return RedirectToAction( "Dashboard" , "Users" );
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
                                        review.Author = (User) _context.User.OrderBy( r => Guid.NewGuid() ).FirstOrDefault(); // Pick randomly user
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

        private async Task<Movie> CreateMovie ( string resultTitle )
            {
            //Define your baseUrl
            Movie movie = new Movie();
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
                            if ( !data.Contains( "Movie not found" ) )
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
                                    await getTrailer( theMovieName );
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
                                    //Insert The movie to DB
                                    _context.Add( movie );
                                    await _context.SaveChangesAsync();
                                    //Create Reviews
                                    if ( _context.User.Any() )
                                        await CreateMovieReviews( movie );
                                    await StoreGenresDP(); // New movie add -> update the Genre dropdown list
                                    return movie;
                                    }
                                else // The movie is in the database 
                                    {
                                    //exist = true;
                                    movie = _context.Movie.FirstOrDefault( p => p.Name.Contains( theMovieName ) );
                                    return movie;
                                    }
                                }
                            else
                                {
                                Console.WriteLine( "NO Data----------" );
                                return null;
                                }
                            }
                        }
                    }
                }
            catch ( Exception exception )
                {
                Console.WriteLine( "Exception Hit------------" );
                Console.WriteLine( exception );
                return null;
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
            if ( query.Any() )
                return View( "Index" , await query.ToListAsync() );
            else
                return View( "NoResults" );
            }

        public async Task getTrailer ( string movieName )
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
        public IActionResult GetGenresDP ( ) // get data form cookies to Genre dropdown button on Layout
            {
            // Insert data to ViewBag for Layout
            //Get the genres from coockies and convert from string to list
            if ( ViewBag.firstcolumn == null )
                if ( HttpContext.Request.Cookies.Where( v => v.Key == "GenresFirstColumn" ).FirstOrDefault().Value != null )
                    ViewBag.firstcolumn = HttpContext.Request.Cookies.Where( v => v.Key == "GenresFirstColumn" ).FirstOrDefault().Value.Split( "," );
            if ( ViewBag.secondcolumn == null )
                if ( HttpContext.Request.Cookies.Where( v => v.Key == "GenresSecondColumn" ).FirstOrDefault().Value != null )
                    ViewBag.secondcolumn = HttpContext.Request.Cookies.Where( v => v.Key == "GenresSecondColumn" ).FirstOrDefault().Value.Split( "," );
            if ( ViewBag.thirdcolumn == null )
                if ( HttpContext.Request.Cookies.Where( v => v.Key == "GenresThirdColumn" ).FirstOrDefault().Value != null )
                    ViewBag.thirdcolumn = HttpContext.Request.Cookies.Where( v => v.Key == "GenresThirdColumn" ).FirstOrDefault().Value.Split( "," );
            return PartialView();
            }

        public async Task StoreGenresDP ( ) // Save data first time or new movie/genre added
            {
            var genres = await _context.Genre.Select( g => g.Type ).ToListAsync();
            int amount = genres.Count;
            if ( amount != 0 )
                {
                List<string> GenresFirstColumn = null;
                List<string> GenresSecondColumn = null;
                List<string> GenresThirdColumn = null;
                if ( amount > 2 ) // If amount of genres more then/equal 3 
                    {
                    int range = (int) Math.Floor( (double) ( amount / 3 ) );
                    switch ( amount % 3 )
                        {
                        case 0: // If amount of genres divide by 3
                            GenresFirstColumn = genres.GetRange( 0 , range );
                            GenresSecondColumn = genres.GetRange( range , range );
                            GenresThirdColumn = genres.GetRange( 2 * ( range ) , range );
                            break;
                        case 1: // If amount of genres divide by 3 with 1 remainder
                            GenresFirstColumn = genres.GetRange( 0 , range + 1 ); // This column will have 1 more genre
                            GenresSecondColumn = genres.GetRange( range + 1 , range );
                            GenresThirdColumn = genres.GetRange( 2 * ( range ) + 1 , range );
                            break;
                        case 2: // If amount of genres divide by 3 with 2 remainder
                            GenresFirstColumn = genres.GetRange( 0 , range + 1 ); // This column will have 1 more genre
                            GenresSecondColumn = genres.GetRange( range + 1 , range + 1 ); // This column will have 1 more genre
                            GenresThirdColumn = genres.GetRange( 2 * ( range ) + 2 , range );
                            break;
                        }
                    }
                else // If amount of genres less then 3
                    {
                    GenresFirstColumn = genres.GetRange( 0 , 1 );
                    if ( amount > 1 ) // If amount of genres equal to 2
                        GenresSecondColumn = genres.GetRange( 1 , 1 );
                    }
                // flatten it into a single string for storing a cookie and store it
                if ( GenresFirstColumn != null )
                    {
                    ViewBag.firstcolumn = GenresFirstColumn;                // Insert data to ViewBag for Layout
                    HttpContext.Response.Cookies.Append( "GenresFirstColumn" , string.Join( "," , GenresFirstColumn ) );
                    }
                if ( GenresSecondColumn != null )
                    {
                    HttpContext.Response.Cookies.Append( "GenresSecondColumn" , string.Join( "," , GenresSecondColumn ) );
                    ViewBag.secondcolumn = GenresSecondColumn;                // Insert data to ViewBag for Layout
                    }
                if ( GenresThirdColumn != null )
                    {
                    HttpContext.Response.Cookies.Append( "GenresThirdColumn" , string.Join( "," , GenresThirdColumn ) );
                    ViewBag.thirdcolumn = GenresThirdColumn;                // Insert data to ViewBag for Layout
                    }
                }
            }

        public async Task<IActionResult> AZlist ( ) //Return the Movies ordered by A-Z
            {
            var movieList = await _context.Movie.OrderBy( m => m.Name ).ToListAsync();
            if ( movieList.Any() )
                return View( "Index" , movieList );
            else
                return View( "NoResults" );
            }
        public async Task<IActionResult> TopMovies ( ) // Return the top 10 Movies by rating 
            {
            var movieList = await _context.Movie.OrderByDescending( m => m.AverageRating ).Take( 10 ).ToListAsync();
            if ( movieList.Any() )
                return View( "Index" , movieList );
            else
                return View( "NoResults" );
            }
        public void SetgenreSuggestions ( List<Genre> newgenres ) // store user or guest Movies Genres suggestions 
            {
            if ( HttpContext.Session.GetString( "UserId" ) != null ) // get the id of Connected user
                SetUserSuggestions( newgenres );
            else // if the user is Guest
                SetGuestSuggestions( newgenres );
            }
        public void SetUserSuggestions ( List<Genre> newgenres ) // Store the Movie Genres suggestions for User
            {
            int connecteduserid = int.Parse( HttpContext.Session.GetString( "UserId" ) ); // Get from session the user id that connected
            User tempUser = _context.User.Where( u => u.Id == connecteduserid ).FirstOrDefault();
            var usergenre = _context.UserGenre.Where( ug => ug.UserId == connecteduserid ); // Get the genres related to user
            foreach ( Genre g in newgenres ) // Loop to update/add the genres to user suggestions
                {
                if ( usergenre.Any( us => us.Genre == g ) ) // If genre is exist
                    {
                    usergenre.Where( us => us.Genre == g ).First().Weight += 1;
                    }
                else // genre doesn't exist
                    {
                    UserGenre userGenre = new UserGenre();
                    userGenre.GenreId = g.Id;
                    userGenre.Genre = g;
                    userGenre.UserId = tempUser.Id;
                    userGenre.User = tempUser;
                    userGenre.Weight = 1;
                    if ( g.UserGenre == null )
                        g.UserGenre = new List<UserGenre>();
                    g.UserGenre.Add( userGenre );
                    if ( tempUser.UserGenre == null )
                        tempUser.UserGenre = new List<UserGenre>();
                    tempUser.UserGenre.Add( userGenre );
                    _context.Add( userGenre );
                    }
                }
            _context.SaveChanges();
            }
        public void SetGuestSuggestions ( List<Genre> newgenres ) // Store the Movie Genres suggestions for Guest
            {
            // Get the Genre Suggestions from cookies
            var cookieSugg = Request.Cookies.Where( v => v.Key == "GuestSuggestions" ).FirstOrDefault().Value;
            var genres = newgenres.Select( g => g.Type ).ToList(); // Convert to list with the names only
            List<String> guestSugg = new List<String>();
            if ( cookieSugg != null )
                {
                guestSugg = cookieSugg.Split( "," ).ToList();
                if ( guestSugg.Count + newgenres.Count < 10 ) // Guest's Suggestions not full (less then 10)
                    {
                    guestSugg.AddRange( genres ); // Add the new genres to the old genres
                    }
                else
                    {
                    guestSugg.RemoveRange( 0 , guestSugg.Count + newgenres.Count - 10 ); // Remove some elemnts for new genres
                    guestSugg.AddRange( genres );
                    }
                }
            else // Ff there isnt Suggestions in cookies
                {
                guestSugg = genres;
                }
            // Save the new genres Suggestions
            HttpContext.Response.Cookies.Append( "GuestSuggestions" , string.Join( "," , guestSugg ) );
            }
        public List<String> GetGuestSuggestions ( ) // return the Movie Genres suggestions for guest
            {
            var cookieSugg = Request.Cookies.Where( v => v.Key == "GuestSuggestions" ).FirstOrDefault().Value;
            List<String> guestSugg = new List<String>();
            if ( cookieSugg != null )
                {
                guestSugg = cookieSugg.Split( "," ).ToList(); // convert to list of strings
                }
            return guestSugg;
            }
        public Dictionary<int , int> GetUserSuggestions ( ) // return the Movie Genres suggestions for user
            {
            int connecteduserid = int.Parse( HttpContext.Session.GetString( "UserId" ) );
            User tempUser = _context.User.Where( u => u.Id == connecteduserid ).FirstOrDefault();
            var usergenre = _context.UserGenre.Where( ug => ug.UserId == connecteduserid ); // Get the genres related to user
            return usergenre.ToDictionary( k => k.GenreId , v => v.Weight );
            }
        public IActionResult MovieSuggestions ( ) // Return the 10 Movie Suggestions for User or Guest
            {
            IDictionary<int , int> moviesWeight = new Dictionary<int , int>();
            var genresMap = new Dictionary<int , int>();
            if ( HttpContext.Session.GetString( "UserId" ) == null ) // if user isn't logged
                {
                var genres = GetGuestSuggestions();// get the genres Suggestions from cookies
                var genresList = genres.Join( _context.Genre , gs => gs , g => g.Type , ( mg , g ) => g ).ToList(); // Join with Genre database to get the genre id
                var tempDictionary = genresList.GroupBy( x => x ) // convert the list of genres to Dictionary - Key:Genre ID(KeyValuePair), Value:count how many on list => weight
               .ToDictionary( y => y.Key.Id , y => y.Count() )
               .OrderByDescending( z => z.Value );
                foreach ( var element in tempDictionary ) // convert Dictionary from KeyValuePair to int as Key
                    {
                    genresMap.Add( element.Key , element.Value );
                    }
                }
            else // user logged
                {
                genresMap = GetUserSuggestions();
                }

            foreach ( MovieGenre mg in _context.MovieGenre ) // Give weight to each movie depend the genres
                {
                if ( moviesWeight.ContainsKey( mg.MovieId ) ) // if a movie already has weight -> update the weight
                    {
                    if ( genresMap.Any( gm => gm.Key == mg.GenreId ) ) // if a genre has weight else does nothing
                        moviesWeight [ mg.MovieId ] += genresMap [ mg.GenreId ];
                    }
                else // if the movie doesn't have weight yet
                    {
                    moviesWeight.Add( mg.MovieId , 0 );
                    if ( genresMap.Any( gm => gm.Key == mg.GenreId ) ) // if a genre has weight
                        moviesWeight [ mg.MovieId ] = genresMap [ mg.GenreId ];
                    }
                }
            var movies = moviesWeight.OrderByDescending( m => m.Value ).Take( 10 ).Join( _context.Movie , mw => mw.Key , m => m.Id , ( mw , m ) => m ).ToList(); // Join with Genre database to get the genre id
            return View( "Index" , movies );
            }
        public void EditGenres ( string genres , int movieId ) // Store the Movie Genres suggestions for Guest
            {
            var oldGenres = _context.MovieGenre.Include( mg => mg.Genre ).Where( mg => mg.MovieId == movieId ).ToList(); // Get the genres related to user
            var movie = _context.Movie.Include( m => m.MovieGenre ).Where( m => m.Id == movieId ).FirstOrDefault();
            var newGenres = genres.Split( "," ); // Split the selected genres to array
            foreach ( string genre in newGenres ) // Check if there is any new genrs to add the movie
                {
                if ( !oldGenres.Any( mg => mg.Genre.Type == genre ) ) // Check if the selected genres already connected to the movie
                    {
                    var newgenre = _context.Genre.Include( g => g.MovieGenre ).Where( g => g.Type == genre ).FirstOrDefault(); // Find the missing genre and add to a list
                    MovieGenre movieGenre = new MovieGenre();
                    movieGenre.GenreId = newgenre.Id;
                    movieGenre.Genre = newgenre;
                    movieGenre.MovieId = movie.Id;
                    movieGenre.Movie = movie;
                    if ( newgenre.MovieGenre == null )
                        newgenre.MovieGenre = new List<MovieGenre>();
                    newgenre.MovieGenre.Add( movieGenre );
                    if ( movie.MovieGenre == null )
                        movie.MovieGenre = new List<MovieGenre>();
                    movie.MovieGenre.Add( movieGenre );
                    _context.Add( movieGenre );
                    }
                }
            foreach ( MovieGenre genre in oldGenres ) // Check if there is any old genres to removed from the movie genres
                {
                if ( !newGenres.Any( mg => mg == genre.Genre.Type ) )
                    {
                    var removedGenre = _context.Genre.Include( g => g.MovieGenre ).Where( g => g.Type == genre.Genre.Type ).FirstOrDefault(); // Find the missing genre and add to a list
                    removedGenre.MovieGenre.Remove( genre );
                    movie.MovieGenre.Remove( genre );
                    _context.MovieGenre.Remove( genre );
                    }
                }
            _context.SaveChanges();
            }

        public void EditActors ( string actors , int movieId ) // Store the Movie Genres suggestions for Guest
            {
            var oldActors = _context.MovieActor.Include( ma => ma.Actor ).Where( ma => ma.MovieId == movieId ).ToList(); // Get the actors related to movie
            var movie = _context.Movie.Include( m => m.MovieGenre ).Where( m => m.Id == movieId ).FirstOrDefault();
            var newActors = actors.Split( "," ); // Split the selected genres to array
            foreach ( string actor in newActors ) // Check if there is any new actors to add the movie
                {
                if ( !oldActors.Any( mg => mg.Actor.Name == actor ) ) // Check if the selected actors already connected to the movie
                    {
                    var newactor = _context.Actor.Include( g => g.MovieActor ).Where( g => g.Name == actor ).FirstOrDefault(); // Find the missing actor and add to a list
                    MovieActor movieActor = new MovieActor();
                    movieActor.ActorId = newactor.Id;
                    movieActor.Actor = newactor;
                    movieActor.MovieId = movie.Id;
                    movieActor.Movie = movie;
                    if ( newactor.MovieActor == null )
                        newactor.MovieActor = new List<MovieActor>();
                    newactor.MovieActor.Add( movieActor );
                    if ( movie.MovieActor == null )
                        movie.MovieActor = new List<MovieActor>();
                    movie.MovieActor.Add( movieActor );
                    _context.Add( movieActor );
                    }
                }
            foreach ( MovieActor actor in oldActors ) // Check if there is any old actors to removed from the movie actors
                {
                if ( !newActors.Any( mg => mg == actor.Actor.Name ) )
                    {
                    var removedActor = _context.Actor.Include( g => g.MovieActor ).Where( g => g.Name == actor.Actor.Name ).FirstOrDefault(); // Find the missing actor and add to a list
                    removedActor.MovieActor.Remove( actor );
                    movie.MovieActor.Remove( actor );
                    _context.MovieActor.Remove( actor );
                    }
                }
            _context.SaveChanges();
            }
        public void Tweet ( string movieName , string releaseDate , string movieDirector , string movieRating )
            {
            string messageToPost = "Hi There !\n" +
                                 "Someone just explored the movie " + movieName + "!\n" +
                                 "Release Date: " + releaseDate + ".\n" +
                                 "Director: " + movieDirector + ".\n" +
                                 "IMDB Rating: " + movieRating + ".\n" +
                                 "Whats a great Movie";

            string twitterURL = "https://api.twitter.com/1.1/statuses/update.json";

            string oauth_consumer_key = "ogrOneaqh9a1HXbUDflObM64N";
            string oauth_consumer_secret = "iZEcLq1fR1Ps60N5G0yP3gcATniK870j1OeeGA82z72hU2i7ku";
            string oauth_token = "1325414546182430721-5fgmOh2pKpy1ZLwkYT1oMjjaQy88w0";
            string oauth_token_secret = "2UZ88dVVgHVorMcXylB4KaXDsXRW1r2srdlVRus2uPbA1";

            // set the oauth version and signature method
            string oauth_version = "1.0";
            string oauth_signature_method = "HMAC-SHA1";

            // create unique request details
            string oauth_nonce = Convert.ToBase64String( new ASCIIEncoding().GetBytes( DateTime.Now.Ticks.ToString() ) );
            System.TimeSpan timeSpan = ( DateTime.UtcNow - new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , 0 , DateTimeKind.Utc ) );
            string oauth_timestamp = Convert.ToInt64( timeSpan.TotalSeconds ).ToString();

            // create oauth signature
            string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";

            string baseString = string.Format(
                baseFormat ,
                oauth_consumer_key ,
                oauth_nonce ,
                oauth_signature_method ,
                oauth_timestamp , oauth_token ,
                oauth_version ,
                Uri.EscapeDataString( messageToPost )
            );

            string oauth_signature = null;
            using ( HMACSHA1 hasher = new HMACSHA1( ASCIIEncoding.ASCII.GetBytes( Uri.EscapeDataString( oauth_consumer_secret ) + "&" + Uri.EscapeDataString( oauth_token_secret ) ) ) )
                {
                oauth_signature = Convert.ToBase64String( hasher.ComputeHash( ASCIIEncoding.ASCII.GetBytes( "POST&" + Uri.EscapeDataString( twitterURL ) + "&" + Uri.EscapeDataString( baseString ) ) ) );
                }

            // create the request header
            string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

            string authorizationHeader = string.Format(
                authorizationFormat ,
                Uri.EscapeDataString( oauth_consumer_key ) ,
                Uri.EscapeDataString( oauth_nonce ) ,
                Uri.EscapeDataString( oauth_signature ) ,
                Uri.EscapeDataString( oauth_signature_method ) ,
                Uri.EscapeDataString( oauth_timestamp ) ,
                Uri.EscapeDataString( oauth_token ) ,
                Uri.EscapeDataString( oauth_version )
            );

            HttpWebRequest objHttpWebRequest = (HttpWebRequest) WebRequest.Create( twitterURL );
            objHttpWebRequest.Headers.Add( "Authorization" , authorizationHeader );
            objHttpWebRequest.Method = "POST";
            objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            using ( Stream objStream = objHttpWebRequest.GetRequestStream() )
                {
                byte [ ] content = ASCIIEncoding.ASCII.GetBytes( "status=" + Uri.EscapeDataString( messageToPost ) );
                objStream.Write( content , 0 , content.Length );
                }

            var responseResult = "";

            try
                {
                //success posting
                WebResponse objWebResponse = objHttpWebRequest.GetResponse();
                StreamReader objStreamReader = new StreamReader( objWebResponse.GetResponseStream() );
                responseResult = objStreamReader.ReadToEnd().ToString();
                }
            catch ( Exception ex )
                {
                responseResult = "Twitter Post Error: " + ex.Message.ToString() + ", authHeader: " + authorizationHeader;
                }
            }
        }
    }