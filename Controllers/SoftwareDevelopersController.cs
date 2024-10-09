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
    public class SoftwareDevelopersController : Controller
    {
        private readonly IRepositoryBase<SoftwareDeveloper> _context;

        public SoftwareDevelopersController(IRepositoryBase<SoftwareDeveloper> context)
        {
            _context = context;
        }

        // GET: SoftwareDevelopers
        public async Task<IActionResult> Index()
        {
            var anModel = await _context.FindAll().ToListAsync();
            return View(anModel);
        }

        // GET: SoftwareDevelopers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var softwareDeveloper = await _context.FindByCondition(a => a.Id == id)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (softwareDeveloper == null)
            {
                return NotFound();
            }

            return View(softwareDeveloper);
        }

        // GET: SoftwareDevelopers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SoftwareDevelopers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameDeveloper,Address,ContactEmail")] SoftwareDeveloper softwareDeveloper)
        {
            if (ModelState.IsValid)
            {
                _context.Create(softwareDeveloper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(softwareDeveloper);
        }

        // GET: SoftwareDevelopers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var softwareDeveloper = await _context.FindByCondition(a => a.Id == id).FirstOrDefaultAsync();
            if (softwareDeveloper == null)
            {
                return NotFound();
            }
            return View(softwareDeveloper);
        }

        // POST: SoftwareDevelopers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameDeveloper,Address,ContactEmail")] SoftwareDeveloper softwareDeveloper)
        {
            if (id != softwareDeveloper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(softwareDeveloper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareDeveloperExists(softwareDeveloper.Id))
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
            return View(softwareDeveloper);
        }

        // GET: SoftwareDevelopers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var softwareDeveloper = await _context.FindByCondition(a => a.Id == id).FirstOrDefaultAsync();
            if (softwareDeveloper == null)
            {
                return NotFound();
            }

            return View(softwareDeveloper);
        }

        // POST: SoftwareDevelopers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anModel = await _context.FindByCondition(t => t.Id == id).FirstOrDefaultAsync();
            if (anModel == null)
            {
                return Problem("Entity set 'GameWORLDContext.SoftwareDeveloperModel'  is null.");
            }

            _context.Delete(anModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoftwareDeveloperExists(int id)
        {
            return _context.FindByCondition(t => t.Id == id).FirstOrDefault() != null;
        }
    }
        /*private readonly GameWORLDContext _context;

        public SoftwareDevelopersController(GameWORLDContext context)
        {
            _context = context;
        }

        // GET: SoftwareDevelopers
        public async Task<IActionResult> Index()
        {
              return _context.SoftwareDeveloper != null ? 
                          View(await _context.SoftwareDeveloper.ToListAsync()) :
                          Problem("Entity set 'GameWORLDContext.SoftwareDeveloper'  is null.");
        }

        // GET: SoftwareDevelopers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SoftwareDeveloper == null)
            {
                return NotFound();
            }

            var softwareDeveloper = await _context.SoftwareDeveloper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (softwareDeveloper == null)
            {
                return NotFound();
            }

            return View(softwareDeveloper);
        }

        // GET: SoftwareDevelopers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SoftwareDevelopers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameDeveloper,Address,ContactEmail")] SoftwareDeveloper softwareDeveloper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(softwareDeveloper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(softwareDeveloper);
        }

        // GET: SoftwareDevelopers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SoftwareDeveloper == null)
            {
                return NotFound();
            }

            var softwareDeveloper = await _context.SoftwareDeveloper.FindAsync(id);
            if (softwareDeveloper == null)
            {
                return NotFound();
            }
            return View(softwareDeveloper);
        }

        // POST: SoftwareDevelopers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameDeveloper,Address,ContactEmail")] SoftwareDeveloper softwareDeveloper)
        {
            if (id != softwareDeveloper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(softwareDeveloper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareDeveloperExists(softwareDeveloper.Id))
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
            return View(softwareDeveloper);
        }

        // GET: SoftwareDevelopers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SoftwareDeveloper == null)
            {
                return NotFound();
            }

            var softwareDeveloper = await _context.SoftwareDeveloper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (softwareDeveloper == null)
            {
                return NotFound();
            }

            return View(softwareDeveloper);
        }

        // POST: SoftwareDevelopers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SoftwareDeveloper == null)
            {
                return Problem("Entity set 'GameWORLDContext.SoftwareDeveloper'  is null.");
            }
            var softwareDeveloper = await _context.SoftwareDeveloper.FindAsync(id);
            if (softwareDeveloper != null)
            {
                _context.SoftwareDeveloper.Remove(softwareDeveloper);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoftwareDeveloperExists(int id)
        {
          return (_context.SoftwareDeveloper?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }*/
}