using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebMongoDB.Data;
using WebMongoDB.Models;

namespace WebMongoDB.Controllers
{
    public class UsersController : Controller
    {
        private readonly WebMongoDBContext _context;

        public UsersController(WebMongoDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            ContextMongodb dbContext = new ContextMongodb();
            return View(await dbContext.User.Find(u => true).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			ContextMongodb dbContext = new ContextMongodb();
			var user = await dbContext.User.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] User user)
        {
            if (ModelState.IsValid)
            {
				ContextMongodb dbContext = new ContextMongodb();
				user.Id = Guid.NewGuid();
                await dbContext.User.InsertOneAsync(user);
				
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			ContextMongodb dbContext = new ContextMongodb();
			var user = await dbContext.User.Find(m => m.Id == id).FirstOrDefaultAsync();
			if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					ContextMongodb dbContext = new ContextMongodb();
                    await dbContext.User.ReplaceOneAsync(m => m.Id == user.Id, user);
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			ContextMongodb dbContext = new ContextMongodb();
			var user = await dbContext.User.Find(m => m.Id == id).FirstOrDefaultAsync();
			if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
			ContextMongodb dbContext = new ContextMongodb();
			await dbContext.User.DeleteOneAsync(m => m.Id == id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
			ContextMongodb dbContext = new ContextMongodb();
			var user = dbContext.User.Find(m => m.Id == id).Any();
            return user;
		}
    }
}
