using Microsoft.AspNetCore.Authentication;
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
            var user = _context.User.FirstOrDefault( u => u.UserName == username && u.Password == password );
            if ( user != null )
                {
                SignIn( user );
                return RedirectToAction( "Homepage" , "Movies" );
                }
            return View();
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

            _context.Add( account );
            await _context.SaveChangesAsync();

            SignIn( account );
            return RedirectToAction( "Index" , "Movies" );
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
            dynamic Multiple = new ExpandoObject();
            Multiple.actors = await _context.Actor.Include( a => a.MovieActor ).ToListAsync();
            Multiple.movies = await _context.Movie.ToListAsync();
            Multiple.users = await _context.User.ToListAsync();
            Multiple.genres = await _context.Genre.Include( g => g.MovieGenre ).ToListAsync();
            //var result = warmCountries.Join( europeanCountries , warm => warm , european => european , ( warm , european ) => warm );
            //var result = ( from a in _context.Actor
            //               join m in _context.Movie on a.MovieId equals m.Id
            //               select m );
            //List<Product> orderedProducts = _context.ProductOrder.Join( _context.Product , x => x.ProductID , y => y.ProductID , ( x , y ) => y ).ToList();

            var result = _context.MovieActor.Join( _context.Actor , x => x.MovieId , y => y.MovieId , ( x , y ) => y ).ToList();

            return View( Multiple );
            }

        // GET: Users
        public async Task<IActionResult> Index ( )
            {
            string user = HttpContext.Session.GetString( "Type" ); //Function to verify the user before get in the view
            if ( user == "Guest" )
                return RedirectToAction( "Login" , "Users" );
            else
                return View( await _context.User.ToListAsync() );
            }

        // GET: Users/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var user = await _context.User
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( user == null )
                {
                return NotFound();
                }

            return View( user );
            }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var user = await _context.User.FindAsync( id );
            if ( user == null )
                {
                return NotFound();
                }
            return View( user );
            }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,UserName,Password,Email,FirstName,LastName,Address" )] User user )
            {
            if ( id != user.Id )
                {
                return NotFound();
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( user );
                    await _context.SaveChangesAsync();
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !UserExists( user.Id ) )
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
            return View( user );
            }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete ( int? id )
            {
            if ( id == null )
                {
                return NotFound();
                }

            var user = await _context.User
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( user == null )
                {
                return NotFound();
                }

            return View( user );
            }

        // POST: Users/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var user = await _context.User.FindAsync( id );
            _context.User.Remove( user );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
            }

        private bool UserExists ( int id )
            {
            return _context.User.Any( e => e.Id == id );
            }
        }
    }
