using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Controllers
    {
    public class UsersController : Controller
        {
        private readonly MovieStoreContext _context;
        public UsersController ( MovieStoreContext context )
            {
            _context = context;
            }

        public IActionResult Login ( )
            {
            return View();
            }

        [HttpPost]
        public IActionResult Login ( string username , string password )
            {
            var user = _context.User.FirstOrDefault( u => u.UserName == username );
            if ( user != null )
                {
                if ( user.Password == password )
                    {
                    SignIn( user );
                    return RedirectToAction( "Homepage" , "Movies" );
                    }
                ViewBag.error = 400; // Bad password was enterd
                return View( "ClientError" );
                }
            ViewBag.error = 404; // Didn't found the user
            return View( "ClientError" );
            }

        private void SignIn ( User user )
            {
            HttpContext.Session.SetString( "Type" , user.Type.ToString() );
            HttpContext.Session.SetString( "UserId" , user.Id.ToString() );
            HttpContext.Session.SetString( "UserName" , user.UserName.ToString() );
            }
        public IActionResult Register ( )
            {
            return View();
            }
        [HttpPost]
        public async Task<IActionResult> Register ( string username , string password , string Email , string FirstName = null , string LastName = null , string Address = null , string type = "Customer" )
            {
            User account = new User() { UserName = username , Password = password , FirstName = FirstName , LastName = LastName , Address = Address , Email = Email };

            if ( type == "Customer" )
                account.Type = UserType.Customer;
            else
                account.Type = UserType.Admin;
            var user = _context.User.FirstOrDefault( u => u.UserName == username );
            if ( user == null ) // If username doesn't exist
                {
                _context.Add( account );
                await _context.SaveChangesAsync();
                SignIn( account );
                return RedirectToAction( "HomePage" , "Movies" );
                }
            ViewBag.error = 400; // Username exist
            return View( "ClientError" );
            }

        public IActionResult Logout ( )
            {
            HttpContext.Session.Remove( "Type" );
            HttpContext.Session.Remove( "UserId" );
            HttpContext.Session.Remove( "UserName" );
            return RedirectToAction( "HomePage" , "Movies" );
            }

        public async Task<IActionResult> Dashboard ( )
            {
            if ( HttpContext.Session.GetString( "Type" ) == "Admin" )
                {
                //Function to verify the user before get in the view)
                dynamic Multiple = new ExpandoObject();
                Multiple.actors = await _context.Actor.Include( a => a.MovieActor ).ToListAsync();
                Multiple.movies = await _context.Movie.ToListAsync();
                Multiple.users = await _context.User.ToListAsync();
                Multiple.genres = await _context.Genre.Include( g => g.MovieGenre ).ToListAsync();
                return View( Multiple );
                }
            else
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            }

        // GET: Users
        public IActionResult Index ( )
            {
            string user = HttpContext.Session.GetString( "Type" ); //Function to verify the user before get in the view
            if ( user == "Guest" ) // User doesnt allowed
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            else
                return RedirectToAction( "Dashboard" , "Users" );
            }

        // GET: Users/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( id == null ) //If id didn't pass
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            var user = await _context.User
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( user == null ) // If user wasn't found
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            return View( user );
            }
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit ( int? id )
            {
            if ( HttpContext.Session.GetString( "UserId" ) == null ) // if user isnt logged
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            int userid = int.Parse( HttpContext.Session.GetString( "UserId" ) );
            if ( id == null )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }
            var user = await _context.User.FindAsync( id );
            if ( user == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }
            if ( user.Id != userid && HttpContext.Session.GetString( "Type" ) == "Customer" ) // If the user is not the author 
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            TempData [ "returnURL" ] = HttpContext.Request.Headers [ "Referer" ].ToString(); // Save the last page viewed to be able to return back to him
            return View( user );
            }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,UserName,Password,Email,FirstName,LastName,Address" )] User user )
            {
            if ( HttpContext.Session.GetString( "UserId" ) == null ) // if user isnt logged
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            int userid = int.Parse( HttpContext.Session.GetString( "UserId" ) );
            if ( id != user.Id ) //If the id doesn't match the user
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    if ( user.Id != userid && HttpContext.Session.GetString( "Type" ) == "Customer" ) // If the user is not the author 
                        {
                        ViewBag.error = 401;
                        return View( "ClientError" );
                        }
                    _context.Update( user );
                    await _context.SaveChangesAsync();
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !UserExists( user.Id ) )
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
            return View( user );
            }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete ( int? id )
            {
            if ( HttpContext.Session.GetString( "UserId" ) == null ) // if user isnt logged
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            int userid = int.Parse( HttpContext.Session.GetString( "UserId" ) );
            if ( id == null )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }
            var user = await _context.User.FindAsync( id );
            if ( user == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }
            if ( user.Id != userid && HttpContext.Session.GetString( "Type" ) == "Customer" ) // If the user is not the author 
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            return View( user );
            }

        // POST: Users/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var user = await _context.User.Include( u => u.Comments ).Include( u => u.UserGenre ).Where( u => u.Id == id ).FirstOrDefaultAsync();
            foreach ( var review in user.Comments )
                {
                _context.Review.Remove( review );
                }
            foreach ( var usergenre in user.UserGenre )
                {
                var genre = await _context.Genre.Include( g => g.UserGenre ).Where( g => g.Id == usergenre.GenreId ).FirstOrDefaultAsync();
                genre.UserGenre.Remove( usergenre );
                _context.UserGenre.Remove( usergenre );
                }
            _context.User.Remove( user );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Dashboard ) );
            }

        private bool UserExists ( int id )
            {
            return _context.User.Any( e => e.Id == id );
            }
        }
    }
