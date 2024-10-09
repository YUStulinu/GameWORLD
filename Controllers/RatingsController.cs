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
    public class RatingsController : Controller
    {
        private readonly IRepositoryBase<Rating> _context;
        private readonly IRepositoryBase<Game> _Game;
        private readonly IRepositoryBase<Customer> _Customer;

        public RatingsController(IRepositoryBase<Rating> context, IRepositoryBase<Game> context1, IRepositoryBase<Customer> context2)
        {
            _context = context;
            _Game = context1;
            _Customer = context2;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var gameWORLDContext = _context.FindAll().Include(r => r.Customer).Include(r => r.Game);
            return View(await gameWORLDContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.FindByCondition(a => a.Id == id)
                .Include(r => r.Customer)
                .Include(r => r.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_Customer.FindAll(), "Id", "Username");
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Title");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Review,Title,PublishDate,Username,GameId,CustomerId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Create(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_Customer.FindAll(), "Id", "Username", rating.CustomerId);
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Title", rating.GameId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.FindByCondition(a => a.Id == id)
                .Include(r => r.Customer)
                .Include(r => r.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_Customer.FindAll(), "Id", "Id", rating.CustomerId);
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Id", rating.GameId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Review,Title,PublishDate,Username,GameId,CustomerId")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
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
            ViewData["CustomerId"] = new SelectList(_Customer.FindAll(), "Id", "Id", rating.CustomerId);
            ViewData["GameId"] = new SelectList(_Game.FindAll(), "Id", "Id", rating.GameId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.FindByCondition(a => a.Id == id)
                .Include(r => r.Customer)
                .Include(r => r.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anModel = await _context.FindByCondition(t => t.Id == id).FirstOrDefaultAsync();
            if (anModel == null)
            {
                return Problem("Entity set 'GameWORLDContext.RatingModel'  is null.");
            }

            _context.Delete(anModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
            return _context.FindByCondition(t => t.Id == id).FirstOrDefault() != null;
        }
    }
        /* private readonly GameWORLDContext _context;

         public RatingsController(GameWORLDContext context)
         {
             _context = context;
         }

         // GET: Ratings
         public async Task<IActionResult> Index()
         {
             var gameWORLDContext = _context.Rating.Include(r => r.Customer).Include(r => r.Game);
             return View(await gameWORLDContext.ToListAsync());
         }

         // GET: Ratings/Details/5
         public async Task<IActionResult> Details(int? id)
         {
             if (id == null || _context.Rating == null)
             {
                 return NotFound();
             }

             var rating = await _context.Rating
                 .Include(r => r.Customer)
                 .Include(r => r.Game)
                 .FirstOrDefaultAsync(m => m.Id == id);
             if (rating == null)
             {
                 return NotFound();
             }

             return View(rating);
         }

         // GET: Ratings/Create
         public IActionResult Create()
         {
             ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Username");
             ViewData["GameId"] = new SelectList(_context.Game, "Id", "Title");
             return View();
         }

         // POST: Ratings/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("Id,Text,Review,Title,PublishDate,Username,GameId,CustomerId")] Rating rating)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(rating);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Username", rating.CustomerId);
             ViewData["GameId"] = new SelectList(_context.Game, "Id", "Title", rating.GameId);
             return View(rating);
         }

         // GET: Ratings/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null || _context.Rating == null)
             {
                 return NotFound();
             }

             var rating = await _context.Rating.FindAsync(id);
             if (rating == null)
             {
                 return NotFound();
             }
             ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", rating.CustomerId);
             ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", rating.GameId);
             return View(rating);
         }

         // POST: Ratings/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Review,Title,PublishDate,Username,GameId,CustomerId")] Rating rating)
         {
             if (id != rating.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(rating);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!RatingExists(rating.Id))
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
             ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", rating.CustomerId);
             ViewData["GameId"] = new SelectList(_context.Game, "Id", "Id", rating.GameId);
             return View(rating);
         }

         // GET: Ratings/Delete/5
         public async Task<IActionResult> Delete(int? id)
         {
             if (id == null || _context.Rating == null)
             {
                 return NotFound();
             }

             var rating = await _context.Rating
                 .Include(r => r.Customer)
                 .Include(r => r.Game)
                 .FirstOrDefaultAsync(m => m.Id == id);
             if (rating == null)
             {
                 return NotFound();
             }

             return View(rating);
         }

         // POST: Ratings/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             if (_context.Rating == null)
             {
                 return Problem("Entity set 'GameWORLDContext.Rating'  is null.");
             }
             var rating = await _context.Rating.FindAsync(id);
             if (rating != null)
             {
                 _context.Rating.Remove(rating);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool RatingExists(int id)
         {
           return (_context.Rating?.Any(e => e.Id == id)).GetValueOrDefault();
         }
     }*/
}