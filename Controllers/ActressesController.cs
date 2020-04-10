using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projects.Models;

namespace Projects.Controllers
{
    public class ActressesController : Controller
    {
        private readonly ProjectsContext _context;

        public ActressesController(ProjectsContext context)
        {
            _context = context;
        }

        // GET: Actresses
        public async Task<IActionResult> Index()
        {
            var projectsContext = _context.Actress.Include(a => a.MovieNameNavigation);
            return View(await projectsContext.ToListAsync());
        }

        // GET: Actresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actress = await _context.Actress
                .Include(a => a.MovieNameNavigation)
                .FirstOrDefaultAsync(m => m.ActressId == id);
            if (actress == null)
            {
                return NotFound();
            }

            return View(actress);
        }

        // GET: Actresses/Create
        public IActionResult Create()
        {
            ViewData["MovieName"] = new SelectList(_context.Movie, "MovieName", "MovieName");
            return View();
        }

        // POST: Actresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActressId,ActressName,Age,NetWorth,MovieName")] Actress actress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieName"] = new SelectList(_context.Movie, "MovieName", "MovieName", actress.MovieName);
            return View(actress);
        }

        // GET: Actresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actress = await _context.Actress.FindAsync(id);
            if (actress == null)
            {
                return NotFound();
            }
            ViewData["MovieName"] = new SelectList(_context.Movie, "MovieName", "MovieName", actress.MovieName);
            return View(actress);
        }

        // POST: Actresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActressId,ActressName,Age,NetWorth,MovieName")] Actress actress)
        {
            if (id != actress.ActressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActressExists(actress.ActressId))
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
            ViewData["MovieName"] = new SelectList(_context.Movie, "MovieName", "MovieName", actress.MovieName);
            return View(actress);
        }

        // GET: Actresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actress = await _context.Actress
                .Include(a => a.MovieNameNavigation)
                .FirstOrDefaultAsync(m => m.ActressId == id);
            if (actress == null)
            {
                return NotFound();
            }

            return View(actress);
        }

        // POST: Actresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actress = await _context.Actress.FindAsync(id);
            _context.Actress.Remove(actress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActressExists(int id)
        {
            return _context.Actress.Any(e => e.ActressId == id);
        }
    }
}
