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
    public class GameGameGenresController : Controller
    {
        private readonly IRepositoryBase<GameGameGenre> _context;
        private readonly IRepositoryBase<Game> _Game;
        private readonly IRepositoryBase<GameGenre> _GameGenre;

        public GameGameGenresController(IRepositoryBase<GameGameGenre> context, IRepositoryBase<Game> context1, IRepositoryBase<GameGenre> context2)
        {
            _context = context;
            _Game = context1;
            _GameGenre = context2;
        }

        // GET: GameGameGenres
        public async Task<IActionResult> Index()
        {
            var gameWORLDContext = _context.FindAll().Include(g => g.Game).Include(g => g.GameGenre);
            return View(await gameWORLDContext.ToListAsync());
        }

        // GET: GameGameGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGameGenre = await _context.FindByCondition(a => a.Id == id)
                .Include(g => g.Game)
                .Include(g => g.GameGenre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameGameGenre == null)
            {
                return NotFound();
            }

            return View(gameGameGenre);
        }

        // GET: GameGameGenres/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Id");
            ViewData["GameGenreId"] = new SelectList(_GameGenre.FindAll(), "Id", "Id");
            return View();
        }

        // POST: GameGameGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameGenreId,GameId")] GameGameGenre gameGameGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Create(gameGameGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Id", gameGameGenre.GameId);
            ViewData["GameGenreId"] = new SelectList(_GameGenre.FindAll(), "Id", "Id", gameGameGenre.GameGenreId);
            return View(gameGameGenre);
        }

        // GET: GameGameGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGameGenre = await _context.FindByCondition(a => a.Id == id)
                .Include(g => g.Game)
                .Include(g => g.GameGenre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameGameGenre == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Id", gameGameGenre.GameId);
            ViewData["GameGenreId"] = new SelectList(_GameGenre.FindAll(), "Id", "Id", gameGameGenre.GameGenreId);
            return View(gameGameGenre);
        }

        // POST: GameGameGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameGenreId,GameId")] GameGameGenre gameGameGenre)
        {
            if (id != gameGameGenre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameGameGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameGameGenreExists(gameGameGenre.Id))
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
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Id", gameGameGenre.GameId);
            ViewData["GameGenreId"] = new SelectList(_GameGenre.FindAll(), "Id", "Id", gameGameGenre.GameGenreId);
            return View(gameGameGenre);
        }

        // GET: GameGameGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGameGenre = await _context.FindByCondition(a => a.Id == id)
                .Include(g => g.Game)
                .Include(g => g.GameGenre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameGameGenre == null)
            {
                return NotFound();
            }

            return View(gameGameGenre);
        }

        // POST: GameGameGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anModel = await _context.FindByCondition(t => t.Id == id).FirstOrDefaultAsync();
            if (anModel == null)
            {
                return Problem("Entity set 'GameWORLDContext.GameGameGenreModel'  is null.");
            }

            _context.Delete(anModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameGameGenreExists(int id)
        {
            return _context.FindByCondition(t => t.Id == id).FirstOrDefault() != null;
        }
    }
        /*private readonly GameWORLDContext _context;

        public GameGameGenresController(GameWORLDContext context)
        {
            _context = context;
        }

        // GET: GameGameGenres
        public async Task<IActionResult> Index()
        {
            var gameWORLDContext = _context.GameGameGenre.Include(g => g.Game).Include(g => g.GameGenre);
            return View(await gameWORLDContext.ToListAsync());
        }

        // GET: GameGameGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GameGameGenre == null)
            {
                return NotFound();
            }

            var gameGameGenre = await _context.GameGameGenre
                .Include(g => g.Game)
                .Include(g => g.GameGenre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameGameGenre == null)
            {
                return NotFound();
            }

            return View(gameGameGenre);
        }

        // GET: GameGameGenres/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id");
            ViewData["GameGenreId"] = new SelectList(_context.GameGenre, "Id", "Id");
            return View();
        }

        // POST: GameGameGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameGenreId,GameId")] GameGameGenre gameGameGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameGameGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameGameGenre.GameId);
            ViewData["GameGenreId"] = new SelectList(_context.GameGenre, "Id", "Id", gameGameGenre.GameGenreId);
            return View(gameGameGenre);
        }

        // GET: GameGameGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GameGameGenre == null)
            {
                return NotFound();
            }

            var gameGameGenre = await _context.GameGameGenre.FindAsync(id);
            if (gameGameGenre == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameGameGenre.GameId);
            ViewData["GameGenreId"] = new SelectList(_context.GameGenre, "Id", "Id", gameGameGenre.GameGenreId);
            return View(gameGameGenre);
        }

        // POST: GameGameGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameGenreId,GameId")] GameGameGenre gameGameGenre)
        {
            if (id != gameGameGenre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameGameGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameGameGenreExists(gameGameGenre.Id))
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
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", gameGameGenre.GameId);
            ViewData["GameGenreId"] = new SelectList(_context.GameGenre, "Id", "Id", gameGameGenre.GameGenreId);
            return View(gameGameGenre);
        }

        // GET: GameGameGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GameGameGenre == null)
            {
                return NotFound();
            }

            var gameGameGenre = await _context.GameGameGenre
                .Include(g => g.Game)
                .Include(g => g.GameGenre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameGameGenre == null)
            {
                return NotFound();
            }

            return View(gameGameGenre);
        }

        // POST: GameGameGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GameGameGenre == null)
            {
                return Problem("Entity set 'GameWORLDContext.GameGameGenre'  is null.");
            }
            var gameGameGenre = await _context.GameGameGenre.FindAsync(id);
            if (gameGameGenre != null)
            {
                _context.GameGameGenre.Remove(gameGameGenre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameGameGenreExists(int id)
        {
          return (_context.GameGameGenre?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }*/
}