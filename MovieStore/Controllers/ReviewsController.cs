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
    public class ReviewsController : Controller
        {
        private readonly MovieStoreContext _context;
        List<Review> reviews = new List<Review>();

        public ReviewsController ( MovieStoreContext context )
            {
            _context = context;
            }

        // GET: Reviews
        public async Task<IActionResult> Index ( )
            {
            return View( await _context.Review.ToListAsync() );
            }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details ( int? id )
            {
            if ( id == null )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            var review = await _context.Review
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( review == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }

            return View( review );
            }

        // GET: Reviews/Create
        public IActionResult Create ( )
            {
            return View();
            }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( [Bind( "Id,Headline,Content,Rating,Published" )] Review review , int movieid )
            {
            if ( ModelState.IsValid )
                {
                review.Published = DateTime.Now;
                review.Movie = _context.Movie.FirstOrDefault( m => m.Id == movieid );
                if ( review.Movie == null )
                    {
                    ViewBag.error = 400;
                    return View( "ClientError" );
                    }
                if ( HttpContext.Session.GetString( "UserId" ) == null ) // Check if any user is connected
                    {
                    ViewBag.error = 400;
                    return View( "ClientError" );
                    }
                int userid = int.Parse( HttpContext.Session.GetString( "UserId" ) );// get the id of Connected user
                review.Author = _context.User.FirstOrDefault( u => u.Id == userid );
                if ( review.Author == null )
                    {
                    ViewBag.error = 400;
                    return View( "ClientError" );
                    }
                _context.Add( review );
                await _context.SaveChangesAsync();
                return RedirectToAction( "Details" , "Movies" , new { id = movieid } );
                }
            return View( review );
            }

        // GET: Reviews/Edit/5
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
            var review = await _context.Review.Include( r => r.Author ).Where( r => r.Id == id ).FirstOrDefaultAsync();
            if ( review == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }
            if ( review.Author.Id != userid && HttpContext.Session.GetString( "Type" ) == "Customer" ) // If the user is not the author 
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            int intReviewRating = (int) Math.Floor( review.Rating ); // 7.3 -> 7
            if ( review.Rating - intReviewRating != 0 && review.Rating - intReviewRating != 0.5 ) // if the rating isn't Integer or double eequal to 0.5
                {
                review.Rating = intReviewRating + 0.5;
                }
            TempData [ "returnURL" ] = HttpContext.Request.Headers [ "Referer" ].ToString(); // Save the last page viewed to be able to return back to him
            return View( review );

            }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,Headline,Content,Rating,Published" )] Review review )
            {
            if ( HttpContext.Session.GetString( "UserId" ) == null ) // if user isnt logged
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            int userid = int.Parse( HttpContext.Session.GetString( "UserId" ) );
            if ( id != review.Id )
                {
                ViewBag.error = 400;
                return View( "ClientError" );
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( review );
                    await _context.SaveChangesAsync();
                    }
                catch ( DbUpdateConcurrencyException )
                    {
                    if ( !ReviewExists( review.Id ) )
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
            return View( review );
            }
        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete ( int? id )
            {
            if ( HttpContext.Session.GetString( "UserId" ) == null ) // if user is logged
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
            var review = await _context.Review.Include( r => r.Author ).Where( r => r.Id == id ).FirstOrDefaultAsync();
            if ( review == null )
                {
                ViewBag.error = 404;
                return View( "ClientError" );
                }
            if ( review.Author.Id != userid && HttpContext.Session.GetString( "Type" ) == "Customer" ) // If the user is not the author 
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            return View( review );
            }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
            {
            var review = await _context.Review.Include( r => r.Movie ).Where( r => r.Id == id ).FirstOrDefaultAsync();
            _context.Review.Remove( review );
            await _context.SaveChangesAsync();
            return RedirectToAction( "Details" , "Movies" , new { id = review.Movie.Id } );
            }

        private bool ReviewExists ( int id )
            {
            return _context.Review.Any( e => e.Id == id );
            }

        public async Task<IActionResult> AdvancedSearch ( string published = "1980 - 2021" , string content = "" , string user = "" )
            {
            int [ ] fromYearsto = Array.ConvertAll( published.Split( " - " ) , y => int.Parse( y ) ); // Convert Release date from string to int array

            // Return the Reviews that qualify these terms
            var query = from r in _context.Review
                        where r.Published.Year >= fromYearsto [ 0 ] && r.Published.Year <= fromYearsto [ 1 ]
                        && r.Content.Contains( content )
                        && ( r.Author.FirstName.Contains( user ) || r.Author.LastName.Contains( user ) )
                        select r;
            return View( "Index" , await query.ToListAsync() );
            }
        }
    }
