using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameWORLD.Data;
using GameWORLD.Models;
using GameWORLD.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GameWORLD.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GameGenresController : Controller
    {
        private readonly IRepositoryBase<GameGenre> _context;

        public GameGenresController(IRepositoryBase<GameGenre> context)
        {
            _context = context;
        }

        // GET: GameGenres
        public async Task<IActionResult> Index()
        {
            var anModel = await _context.FindAll().ToListAsync();
            return View(anModel);
        }

        // GET: GameGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _context.FindByCondition(a=>a.Id == id)
                .FirstOrDefaultAsync();
            if (gameGenre == null)
            {
                return NotFound();
            }

            return View(gameGenre);
        }

        // GET: GameGenres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameCategory,Description")] GameGenre gameGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Create(gameGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameGenre);
        }

        // GET: GameGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _context.FindByCondition(a => a.Id == id).FirstOrDefaultAsync();
            if (gameGenre == null)
            {
                return NotFound();
            }
            return View(gameGenre);
        }

        // POST: GameGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameCategory,Description")] GameGenre gameGenre)
        {
            if (id != gameGenre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameGenreExists(gameGenre.Id))
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
            return View(gameGenre);
        }

        // GET: GameGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _context.FindByCondition(a => a.Id == id).FirstOrDefaultAsync();
            if (gameGenre == null)
            {
                return NotFound();
            }

            return View(gameGenre);
        }

        // POST: GameGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anModel = await _context.FindByCondition(t => t.Id == id).FirstOrDefaultAsync();
            if (anModel == null)
            {
                return Problem("Entity set 'GameWORLDContext.GameGenreModel'  is null.");
            }

            _context.Delete(anModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameGenreExists(int id)
        {
            return _context.FindByCondition(t => t.Id == id).FirstOrDefault() != null;
        }
    }
    /* private readonly GameWORLDContext _context;

     public GameGenresController(GameWORLDContext context)
     {
         _context = context;
     }

     // GET: GameGenres
     public async Task<IActionResult> Index()
     {
           return _context.GameGenre != null ? 
                       View(await _context.GameGenre.ToListAsync()) :
                       Problem("Entity set 'GameWORLDContext.GameGenre'  is null.");
     }

     // GET: GameGenres/Details/5
     public async Task<IActionResult> Details(int? id)
     {
         if (id == null || _context.GameGenre == null)
         {
             return NotFound();
         }

         var gameGenre = await _context.GameGenre
             .FirstOrDefaultAsync(m => m.Id == id);
         if (gameGenre == null)
         {
             return NotFound();
         }

         return View(gameGenre);
     }

     // GET: GameGenres/Create
     public IActionResult Create()
     {
         return View();
     }

     // POST: GameGenres/Create
     // To protect from overposting attacks, enable the specific properties you want to bind to.
     // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Create([Bind("Id,NameCategory,Description")] GameGenre gameGenre)
     {
         if (ModelState.IsValid)
         {
             _context.Add(gameGenre);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }
         return View(gameGenre);
     }

     // GET: GameGenres/Edit/5
     public async Task<IActionResult> Edit(int? id)
     {
         if (id == null || _context.GameGenre == null)
         {
             return NotFound();
         }

         var gameGenre = await _context.GameGenre.FindAsync(id);
         if (gameGenre == null)
         {
             return NotFound();
         }
         return View(gameGenre);
     }

     // POST: GameGenres/Edit/5
     // To protect from overposting attacks, enable the specific properties you want to bind to.
     // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Edit(int id, [Bind("Id,NameCategory,Description")] GameGenre gameGenre)
     {
         if (id != gameGenre.Id)
         {
             return NotFound();
         }

         if (ModelState.IsValid)
         {
             try
             {
                 _context.Update(gameGenre);
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!GameGenreExists(gameGenre.Id))
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
         return View(gameGenre);
     }

     // GET: GameGenres/Delete/5
     public async Task<IActionResult> Delete(int? id)
     {
         if (id == null || _context.GameGenre == null)
         {
             return NotFound();
         }

         var gameGenre = await _context.GameGenre
             .FirstOrDefaultAsync(m => m.Id == id);
         if (gameGenre == null)
         {
             return NotFound();
         }

         return View(gameGenre);
     }

     // POST: GameGenres/Delete/5
     [HttpPost, ActionName("Delete")]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> DeleteConfirmed(int id)
     {
         if (_context.GameGenre == null)
         {
             return Problem("Entity set 'GameWORLDContext.GameGenre'  is null.");
         }
         var gameGenre = await _context.GameGenre.FindAsync(id);
         if (gameGenre != null)
         {
             _context.GameGenre.Remove(gameGenre);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
     }

     private bool GameGenreExists(int id)
     {
       return (_context.GameGenre?.Any(e => e.Id == id)).GetValueOrDefault();
     }
 }*/
}