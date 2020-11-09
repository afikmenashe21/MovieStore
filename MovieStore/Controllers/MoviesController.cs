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
using System.Net;
using System.Collections.Immutable;
using Microsoft.VisualBasic;
using MovieStore.Models.UserPreferences;
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
            string user = HttpContext.Session.GetString( "Type" );
            var movielist = await _context.Movie.ToListAsync();
            movielist.Reverse();
            await StoreGenresDP(); // Trim the list of Genres to 3 columns
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

            ViewBag.reviews = await _context.Review.Where( r => r.Movie.Id == id ).Include( r => r.Author ).ToListAsync();
            ViewBag.reviews.Reverse();
            // Linq - first filter the rows in MovieGenre and then join to get the Genres

            ViewBag.genres = await _context.MovieGenre.Where( mg => mg.MovieId == id ).Join( _context.Genre , mg => mg.GenreId , g => g.Id , ( mg , g ) => g ).ToListAsync();

            // Linq - first filter the rows in MovieActor and then join to get the Actors
            ViewBag.actors = await _context.MovieActor.Where( ma => ma.MovieId == id ).Join( _context.Actor , ma => ma.ActorId , a => a.Id , ( ma , a ) => a ).ToListAsync();
            //SetgenreSuggestions( ViewBag.genres );
            SetgenreSuggestions( ViewBag.genres );
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
                                    //await Run( theMovieName );
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
                                    await StoreGenresDP(); // New movie add -> update the Genre dropdown list
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
        public async Task<IActionResult> GetGenresDP ( ) // get data form cookies to Genre dropdown button on Layout
            {
            // Insert data to ViewBag for Layout
            //Get the genres from coockies and convert from string to list
            if ( ViewBag.firstcolumn == null )
                ViewBag.firstcolumn = HttpContext.Request.Cookies.Where( v => v.Key == "GenresFirstColumn" ).FirstOrDefault().Value.Split( "," );
            if ( ViewBag.secondcolumn == null )
                ViewBag.secondcolumn = HttpContext.Request.Cookies.Where( v => v.Key == "GenresSecondColumn" ).FirstOrDefault().Value.Split( "," );
            if ( ViewBag.thirdcolumn == null )
                ViewBag.thirdcolumn = HttpContext.Request.Cookies.Where( v => v.Key == "GenresThirdColumn" ).FirstOrDefault().Value.Split( "," );
            return PartialView();
            }

        public async Task StoreGenresDP ( ) // Save data first time or new movie/genre added
            {
            var genres = await _context.Genre.Select( g => g.Type ).ToListAsync();
            List<string> GenresFirstColumn = null;
            List<string> GenresSecondColumn = null;
            List<string> GenresThirdColumn = null;
            int amount = genres.Count;
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
            HttpContext.Response.Cookies.Append( "GenresFirstColumn" , string.Join( "," , GenresFirstColumn ) );
            HttpContext.Response.Cookies.Append( "GenresSecondColumn" , string.Join( "," , GenresSecondColumn ) );
            HttpContext.Response.Cookies.Append( "GenresThirdColumn" , string.Join( "," , GenresThirdColumn ) );
            // Insert data to ViewBag for Layout
            ViewBag.firstcolumn = GenresFirstColumn;
            ViewBag.secondcolumn = GenresSecondColumn;
            ViewBag.thirdcolumn = GenresThirdColumn;
            }

        public async Task<IActionResult> AZlist ( ) //Return the Movies ordered by A-Z
            {
            return View( "Index" , await _context.Movie.OrderBy( m => m.Name ).ToListAsync() );
            }
        public async Task<IActionResult> TopMovies ( ) // Return the top 10 Movies by rating 
            {
            return View( "Index" , await _context.Movie.OrderByDescending( m => m.AverageRating ).Take( 10 ).ToListAsync() );
            }
        public async Task SetgenreSuggestions ( List<Genre> newgenres ) // store user or guest Movies Genres suggestions 
            {
            if ( HttpContext.Session.GetString( "UserId" ) != null ) // get the id of Connected user
                await SetUserSuggestions( newgenres );
            else // if the user is Guest
                await SetGuestSuggestions( newgenres );
            }
        public async Task SetUserSuggestions ( List<Genre> newgenres ) // Store the Movie Genres suggestions for User
            {
            int connecteduserid = int.Parse( HttpContext.Session.GetString( "UserId" ) ); // Get from session the user id that connected
            User tempUser = _context.User.Where( u => u.Id == connecteduserid ).First();
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
            await _context.SaveChangesAsync();
            }
        public async Task SetGuestSuggestions ( List<Genre> newgenres ) // Store the Movie Genres suggestions for Guest
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
            User tempUser = _context.User.Where( u => u.Id == connecteduserid ).First();
            var usergenre = _context.UserGenre.Where( ug => ug.UserId == connecteduserid ); // Get the genres related to user
            return usergenre.ToDictionary( k => k.GenreId , v => v.Weight );
            }
        public async Task<IActionResult> MovieSuggestions ( ) // Return the 10 Movie Suggestions for User or Guest
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

        }
    }