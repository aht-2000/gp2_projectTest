using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gp2_project.Data;
using gp2_project.Models;

namespace gp2_project.Controllers
{
    public class managersController : Controller
    {
        private readonly gp2_projectContext _context;

        public managersController(gp2_projectContext context)
        {
            _context = context;
        }

        // GET: managers
        public async Task<IActionResult> Index()
        {
              return _context.manager != null ? 
                          View(await _context.manager.ToListAsync()) :
                          Problem("Entity set 'gp2_projectContext.manager'  is null.");
        }

        // GET: managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.manager == null)
            {
                return NotFound();
            }

            var manager = await _context.manager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,email,password,role,phoneNo")] manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        // GET: managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.manager == null)
            {
                return NotFound();
            }

            var manager = await _context.manager.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,email,password,role,phoneNo")] manager manager)
        {
            if (id != manager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!managerExists(manager.Id))
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
            return View(manager);
        }

        // GET: managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.manager == null)
            {
                return NotFound();
            }

            var manager = await _context.manager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.manager == null)
            {
                return Problem("Entity set 'gp2_projectContext.manager'  is null.");
            }
            var manager = await _context.manager.FindAsync(id);
            if (manager != null)
            {
                _context.manager.Remove(manager);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool managerExists(int id)
        {
          return (_context.manager?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
