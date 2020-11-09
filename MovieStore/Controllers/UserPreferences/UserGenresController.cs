using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Models.UserPreferences;

namespace MovieStore.Controllers.UserPreferences
{
    public class UserGenresController : Controller
    {
        private readonly MovieStoreContext _context;

        public UserGenresController(MovieStoreContext context)
        {
            _context = context;
        }

        // GET: UserGenres
        public async Task<IActionResult> Index()
        {
            var movieStoreContext = _context.UserGenre.Include(u => u.Genre).Include(u => u.User);
            return View(await movieStoreContext.ToListAsync());
        }

        // GET: UserGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGenre = await _context.UserGenre
                .Include(u => u.Genre)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userGenre == null)
            {
                return NotFound();
            }

            return View(userGenre);
        }

        // GET: UserGenres/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address");
            return View();
        }

        // POST: UserGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,GenreId,Weight")] UserGenre userGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type", userGenre.GenreId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address", userGenre.UserId);
            return View(userGenre);
        }

        // GET: UserGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGenre = await _context.UserGenre.FindAsync(id);
            if (userGenre == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type", userGenre.GenreId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address", userGenre.UserId);
            return View(userGenre);
        }

        // POST: UserGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,GenreId,Weight")] UserGenre userGenre)
        {
            if (id != userGenre.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGenreExists(userGenre.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Type", userGenre.GenreId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Address", userGenre.UserId);
            return View(userGenre);
        }

        // GET: UserGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGenre = await _context.UserGenre
                .Include(u => u.Genre)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userGenre == null)
            {
                return NotFound();
            }

            return View(userGenre);
        }

        // POST: UserGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userGenre = await _context.UserGenre.FindAsync(id);
            _context.UserGenre.Remove(userGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGenreExists(int id)
        {
            return _context.UserGenre.Any(e => e.UserId == id);
        }

        public Dictionary<string , int> Graph ( ) // return dic -> key:genre name , value: counter else 0
            {
            Dictionary<string , int> queryDic;
            var queryList = _context.Genre.ToList();
            queryDic = queryList.ToDictionary( k => k.Type , v => 0 ); // get Dictionary => key:genre name , value: 0
            if ( _context.UserGenre.Any() ) // check if data is empty or not
                {
                var queryUserGenre = _context.UserGenre.Include( ug => ug.Genre ).ToList(); // Get the data that reprasent popularity
                var UserGenreDic = queryUserGenre.GroupBy( ug => ug.Genre.Type ).ToDictionary( k => k.Key , v => v.Count() ); // Create Dictionary of all the data => key:genre name , value: counter
                foreach (var genre in UserGenreDic ) // Update the data
                    {
                    queryDic [ genre.Key ] = genre.Value;
                    }
                }
            return queryDic;
            }
        }
}
