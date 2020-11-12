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
                ViewBag.errr = 404;
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
        public async Task<IActionResult> Create ( [Bind( "Id,Name" )] Actor actor )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( ModelState.IsValid )
                {
                _context.Add( actor );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
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
            return View( actor );
            }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id , [Bind( "Id,Name" )] Actor actor )
            {
            if ( HttpContext.Session.GetString( "Type" ) == null || HttpContext.Session.GetString( "Type" ) != "Admin" )
                {
                ViewBag.error = 401;
                return View( "ClientError" );
                }
            if ( id != actor.Id )
                {
                ViewBag.errr = 400;
                return View( "ClientError" );
                }

            if ( ModelState.IsValid )
                {
                try
                    {
                    _context.Update( actor );
                    await _context.SaveChangesAsync();
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
                return RedirectToAction( "Dashboard" , "Users" );
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
                ViewBag.errr = 400;
                return View( "ClientError" );
                }

            var actor = await _context.Actor
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( actor == null )
                {
                ViewBag.errr = 404;
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
            var actor = await _context.Actor.FindAsync( id );
            _context.Actor.Remove( actor );
            await _context.SaveChangesAsync();
            return RedirectToAction( "Dashboard" , "Users" );
            }

        private bool ActorExists ( int id )
            {
            return _context.Actor.Any( e => e.Id == id );
            }
        }
    }
