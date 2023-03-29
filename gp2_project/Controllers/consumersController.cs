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
    public class consumersController : Controller
    {
        private readonly gp2_projectContext _context;

        public consumersController(gp2_projectContext context)
        {
            _context = context;
        }

        // GET: consumers
        public async Task<IActionResult> Index()
        {
              return _context.consumer != null ? 
                          View(await _context.consumer.ToListAsync()) :
                          Problem("Entity set 'gp2_projectContext.consumer'  is null.");
        }

        // GET: consumers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.consumer == null)
            {
                return NotFound();
            }

            var consumer = await _context.consumer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumer == null)
            {
                return NotFound();
            }

            return View(consumer);
        }

        // GET: consumers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: consumers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,email,password,role,phoneNo,location")] consumer consumer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consumer);
        }

        // GET: consumers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.consumer == null)
            {
                return NotFound();
            }

            var consumer = await _context.consumer.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }
            return View(consumer);
        }

        // POST: consumers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,email,password,role,phoneNo,location")] consumer consumer)
        {
            if (id != consumer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!consumerExists(consumer.Id))
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
            return View(consumer);
        }

        // GET: consumers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.consumer == null)
            {
                return NotFound();
            }

            var consumer = await _context.consumer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumer == null)
            {
                return NotFound();
            }

            return View(consumer);
        }

        // POST: consumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.consumer == null)
            {
                return Problem("Entity set 'gp2_projectContext.consumer'  is null.");
            }
            var consumer = await _context.consumer.FindAsync(id);
            if (consumer != null)
            {
                _context.consumer.Remove(consumer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool consumerExists(int id)
        {
          return (_context.consumer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
