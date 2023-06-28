using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Broadband.Data;
using Broadband.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Broadband.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserMessagesController : Controller
    {
        private readonly UserMessageDbContext _context;

        public UserMessagesController(UserMessageDbContext context)
        {
            _context = context;
        }

        // GET: UserMessages
        public async Task<IActionResult> Index()
        {
              return _context.Messages != null ? 
                          View(await _context.Messages.ToListAsync()) :
                          Problem("Entity set 'UserMessageDbContext.Messages'  is null.");
        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var userMessage = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // GET: UserMessages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Query")] UserMessage userMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userMessage);
        }

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var userMessage = await _context.Messages.FindAsync(id);
            if (userMessage == null)
            {
                return NotFound();
            }
            return View(userMessage);
        }

        // POST: UserMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Query")] UserMessage userMessage)
        {
            if (id != userMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMessageExists(userMessage.Id))
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
            return View(userMessage);
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var userMessage = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // POST: UserMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Messages == null)
            {
                return Problem("Entity set 'UserMessageDbContext.Messages'  is null.");
            }
            var userMessage = await _context.Messages.FindAsync(id);
            if (userMessage != null)
            {
                _context.Messages.Remove(userMessage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMessageExists(int id)
        {
          return (_context.Messages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
