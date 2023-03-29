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
using System.Net.Mail;

namespace gp2_project.Controllers
{
    public class UsersController : Controller
    {
        private readonly gp2_projectContext _context;

        public UsersController(gp2_projectContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Home()
        {
            return View(await _context.user.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.user.ToListAsync());
        }
        public IActionResult email()
        {
            return View();
        }

        [HttpPost, ActionName("email")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> email(string address, string subject, string body)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("tu4102546@taibahu.edu.sa");
            mail.To.Add(address); // receiver email address
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = 5250;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("tu4102546@taibahu.edu.sa", "pppp");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            ViewData["Message"] = "Email sent.";
            return View();
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,password,email")] user user)
        {
            user.role = "consumer";

            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(login));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var users = await _context.user.FindAsync(id);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,password,role,email")] user users)
        {
            _context.Update(users);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(login));
        }
        private bool UsersExists(int id)
        {
            return _context.user.Any(e => e.Id == id);
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string na, string pa)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Abdulaziz\\Documents\\gp_db.mdf;Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "SELECT * FROM user where name ='" + na + "' and  password ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

             if (reader.Read())
             {
                 string role = (string)reader["role"];
                 string id = Convert.ToString((int)reader["Id"]);
                 HttpContext.Session.SetString("name", na);
                 HttpContext.Session.SetString("role", role);
                 HttpContext.Session.SetString("id", id); 
                 reader.Close();
                 conn1.Close();
                 if (role == "consumer")

                     return RedirectToAction("Home", "Index");

                 else
                     return RedirectToAction("Index", "Home");
             }
             else
             {
                 ViewData["Message"] = "Wrong username or password!";
                 return View();
             }
        }
        }
    }


