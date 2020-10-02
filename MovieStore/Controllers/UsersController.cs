﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MovieStore.Data;
using MovieStore.Models;

namespace MovieStore.Controllers
    {
    public class UsersController : Controller
        {
        private readonly MovieStoreContext _context;
        public UsersController ( MovieStoreContext context)
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
                return RedirectToAction( "Index" , "Movies" );
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
        public async Task<IActionResult> Register ( string username , string password , string FirstName , string LastName , string address , string Email , string type = "Customer" )
            {
            User account = new User() { UserName = username , Password = password , FirstName = FirstName , LastName = LastName , Address = address , Email = Email };

            if ( type == "Customer" )
                account.Type = UserType.Customer;
            else
                account.Type = UserType.Admin;

            _context.Add( account );
            await _context.SaveChangesAsync();

            SignIn( account );
            return RedirectToAction( "Index" , "Movies" );
            }
        [HttpPost]
        public async Task<IActionResult> Register ( string username , string password , string Email)
            {
            User account = new User() { UserName = username , Password = password , Email = Email , Type = UserType.Customer };

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
            return RedirectToAction( "Index" , "Movies" );
            }

        public IActionResult Dashboard ( )
            {
            dynamic Multiple = new ExpandoObject();
            Multiple.actors = _context.Actor.ToList();
            Multiple.movies = _context.Movie.ToList();
            Multiple.users = _context.User.ToList();
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
