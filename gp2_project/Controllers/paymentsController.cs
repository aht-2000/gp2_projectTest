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
    public class paymentsController : Controller
    {
        private readonly gp2_projectContext _context;

        public paymentsController(gp2_projectContext context)
        {
            _context = context;
        }

        // GET: payments
        public async Task<IActionResult> Index()
        {
              return _context.payment != null ? 
                          View(await _context.payment.ToListAsync()) :
                          Problem("Entity set 'gp2_projectContext.payment'  is null.");
        }

        // GET: payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.payment == null)
            {
                return NotFound();
            }

            var payment = await _context.payment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,orderNo,nameOnCard,bankAccNo,total")] payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.payment == null)
            {
                return NotFound();
            }

            var payment = await _context.payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,orderNo,nameOnCard,bankAccNo,total")] payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!paymentExists(payment.Id))
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
            return View(payment);
        }

        // GET: payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.payment == null)
            {
                return NotFound();
            }

            var payment = await _context.payment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.payment == null)
            {
                return Problem("Entity set 'gp2_projectContext.payment'  is null.");
            }
            var payment = await _context.payment.FindAsync(id);
            if (payment != null)
            {
                _context.payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool paymentExists(int id)
        {
          return (_context.payment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
