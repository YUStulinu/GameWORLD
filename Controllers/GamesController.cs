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
    public class GamesController : Controller
    {
        private readonly IRepositoryBase<Game> _context;
        private readonly IRepositoryBase<SoftwareDeveloper> _SoftwareDeveloper;
        private readonly IRepositoryBase<PublishingCompany> _PublishingCompany;

        public GamesController(IRepositoryBase<Game> context,IRepositoryBase<SoftwareDeveloper> context1 , IRepositoryBase<PublishingCompany> context2)
        {
            _context = context;
            _SoftwareDeveloper = context1;
            _PublishingCompany = context2;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var gameWORLDContext = _context.FindAll().Include(g => g.PublishingCompany).Include(g => g.SoftwareDeveloper);
            return View(await gameWORLDContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.FindByCondition(a => a.Id == id)
                .Include(g => g.PublishingCompany)
                .Include(g => g.SoftwareDeveloper)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["PublishingCompanyId"] = new SelectList(_PublishingCompany.FindAll(), "Id", "NameCompany");
            ViewData["SoftwareDeveloperId"] = new SelectList(_SoftwareDeveloper.FindAll(), "Id", "NameDeveloper");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,ReleaseDate,SoftwareDeveloperId,PublishingCompanyId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Create(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublishingCompanyId"] = new SelectList(_PublishingCompany.FindAll(), "Id", "NameCompany", game.PublishingCompanyId);
            ViewData["SoftwareDeveloperId"] = new SelectList(_SoftwareDeveloper.FindAll(), "Id", "NameDeveloper", game.SoftwareDeveloperId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.FindByCondition(a => a.Id == id)
                .Include(g => g.PublishingCompany)
                .Include(g => g.SoftwareDeveloper)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["PublishingCompanyId"] = new SelectList(_PublishingCompany.FindAll(), "Id", "Id", game.PublishingCompanyId);
            ViewData["SoftwareDeveloperId"] = new SelectList(_SoftwareDeveloper.FindAll(), "Id", "Id", game.SoftwareDeveloperId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ReleaseDate,SoftwareDeveloperId,PublishingCompanyId")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["PublishingCompanyId"] = new SelectList(_PublishingCompany.FindAll(), "Id", "Id", game.PublishingCompanyId);
            ViewData["SoftwareDeveloperId"] = new SelectList(_SoftwareDeveloper.FindAll(), "Id", "Id", game.SoftwareDeveloperId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.FindByCondition(a => a.Id == id)
                .Include(g => g.PublishingCompany)
                .Include(g => g.SoftwareDeveloper)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anModel = await _context.FindByCondition(t => t.Id == id).FirstOrDefaultAsync();
            if (anModel == null)
            {
                return Problem("Entity set 'GameWORLDContext.GameModel'  is null.");
            }

            _context.Delete(anModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.FindByCondition(t => t.Id == id).FirstOrDefault() != null;
        }
    }
        /*private readonly GameWORLDContext _context;

        public GamesController(GameWORLDContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var gameWORLDContext = _context.Game.Include(g => g.PublishingCompany).Include(g => g.SoftwareDeveloper);
            return View(await gameWORLDContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Game == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.PublishingCompany)
                .Include(g => g.SoftwareDeveloper)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["PublishingCompanyId"] = new SelectList(_context.PublishingCompany, "Id", "NameCompany");
            ViewData["SoftwareDeveloperId"] = new SelectList(_context.SoftwareDeveloper, "Id", "NameDeveloper");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,ReleaseDate,SoftwareDeveloperId,PublishingCompanyId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublishingCompanyId"] = new SelectList(_context.PublishingCompany, "Id", "NameCompany", game.PublishingCompanyId);
            ViewData["SoftwareDeveloperId"] = new SelectList(_context.SoftwareDeveloper, "Id", "NameDeveloper", game.SoftwareDeveloperId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Game == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["PublishingCompanyId"] = new SelectList(_context.PublishingCompany, "Id", "Id", game.PublishingCompanyId);
            ViewData["SoftwareDeveloperId"] = new SelectList(_context.SoftwareDeveloper, "Id", "Id", game.SoftwareDeveloperId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ReleaseDate,SoftwareDeveloperId,PublishingCompanyId")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["PublishingCompanyId"] = new SelectList(_context.PublishingCompany, "Id", "Id", game.PublishingCompanyId);
            ViewData["SoftwareDeveloperId"] = new SelectList(_context.SoftwareDeveloper, "Id", "Id", game.SoftwareDeveloperId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Game == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.PublishingCompany)
                .Include(g => g.SoftwareDeveloper)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Game == null)
            {
                return Problem("Entity set 'GameWORLDContext.Game'  is null.");
            }
            var game = await _context.Game.FindAsync(id);
            if (game != null)
            {
                _context.Game.Remove(game);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
          return (_context.Game?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }*/
}