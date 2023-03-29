using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gp2_project.Data;
using gp2_project.Models;
using Microsoft.Data.SqlClient;
using static NuGet.Packaging.PackagingConstants;

namespace gp2_project.Controllers
{
    public class OrdersController : Controller
    {
        private readonly gp2_projectContext _context;

        public OrdersController(gp2_projectContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.orders == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int? id)
        {
            var item = await _context.orders.FindAsync(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int orderNo, int quantity)
        {
            orders order = new orders();
            order.orderNo = orderNo;
            order.orderQty = quantity;

           // order.userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
           // order.buy_date = DateTime.Today;

            _context.Add(order);
            await _context.SaveChangesAsync();

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Abdulaziz\\Documents\\gp_db.mdf;Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "UPDATE Items SET orders.orderQty  = orders.orderQty - '" + order.orderQty + "'  where (orderNo ='" + order.orderNo + "' )";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();

            return RedirectToAction(nameof(orderList));
        }

        public async Task<IActionResult> orderList()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var orItems = await _context.orders.FromSqlRaw("select *  from orders where  orderNo = '" + id + "'  ").ToListAsync();
            return View(orItems);

        }

       /* public async Task<IActionResult> customerOrders(int? id)
        {
            var orItems = await _context.orders.FromSqlRaw("select *  from orders where  orderNo = '" + id + "'  ").ToListAsync();
            return View(orItems);
        }
        public async Task<IActionResult> customerDocument()
        {
            var orItems = await _context.Document.FromSqlRaw("select users.id as Id, users.name as customername, sum (orders.quantity * Price)  as total from items,orders,users where users.id = orders.userid and itemid= items.Id group by users.name,users.id").ToListAsync();

            return View(orItems);
        }*/


        // GET: Orders/Edit/5

        /*public async Task<IActionResult> Edit(int? orderNo)
        {
            if (orderNo == null || _context.orders == null)
            {
                return NotFound();
            }

            var orders = await _context.orders.FindAsync(orderNo);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }*/

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int orderNo, [Bind("orderNo,orderWeight,orderQty")] orders orders)
        {
            if (orderNo != orders.orderNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.orderNo))
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
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.orders == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.orders == null)
            {
                return Problem("Entity set 'gp2_projectContext.Orders'  is null.");
            }
            var orders = await _context.orders.FindAsync(id);
            if (orders != null)
            {
                _context.orders.Remove(orders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool OrdersExists(int id)
        {
            return _context.orders.Any(e => e.Id == id);
        }
    }
}
