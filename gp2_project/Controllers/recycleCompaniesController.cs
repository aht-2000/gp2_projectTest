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
    public class recycleCompaniesController : Controller
    {
        private readonly gp2_projectContext _context;

        public recycleCompaniesController(gp2_projectContext context)
        {
            _context = context;
        }

        // GET: recycleCompanies
        public async Task<IActionResult> Index()
        {
              return _context.recycleCompanies != null ? 
                          View(await _context.recycleCompanies.ToListAsync()) :
                          Problem("Entity set 'gp2_projectContext.recycleCompanies'  is null.");
        }

        // GET: recycleCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.recycleCompanies == null)
            {
                return NotFound();
            }

            var recycleCompanies = await _context.recycleCompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recycleCompanies == null)
            {
                return NotFound();
            }

            return View(recycleCompanies);
        }

        // GET: recycleCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: recycleCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,emai,password,role,companyName,companyNo")] recycleCompanies recycleCompanies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recycleCompanies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recycleCompanies);
        }

        // GET: recycleCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.recycleCompanies == null)
            {
                return NotFound();
            }

            var recycleCompanies = await _context.recycleCompanies.FindAsync(id);
            if (recycleCompanies == null)
            {
                return NotFound();
            }
            return View(recycleCompanies);
        }

        // POST: recycleCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,emai,password,role,companyName,companyNo")] recycleCompanies recycleCompanies)
        {
            if (id != recycleCompanies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recycleCompanies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!recycleCompaniesExists(recycleCompanies.Id))
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
            return View(recycleCompanies);
        }

        // GET: recycleCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.recycleCompanies == null)
            {
                return NotFound();
            }

            var recycleCompanies = await _context.recycleCompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recycleCompanies == null)
            {
                return NotFound();
            }

            return View(recycleCompanies);
        }

        // POST: recycleCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.recycleCompanies == null)
            {
                return Problem("Entity set 'gp2_projectContext.recycleCompanies'  is null.");
            }
            var recycleCompanies = await _context.recycleCompanies.FindAsync(id);
            if (recycleCompanies != null)
            {
                _context.recycleCompanies.Remove(recycleCompanies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool recycleCompaniesExists(int id)
        {
          return (_context.recycleCompanies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
