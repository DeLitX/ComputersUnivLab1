using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputersUnivLab1;

namespace ComputersUnivLab1.Controllers
{
    public class SocketsController : Controller
    {
        private readonly ComputersContext _context;

        public SocketsController(ComputersContext context)
        {
            _context = context;
        }

        // GET: Sockets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sockets.ToListAsync());
        }

        // GET: Sockets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socket = await _context.Sockets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socket == null)
            {
                return NotFound();
            }

            return View(socket);
        }

        // GET: Sockets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sockets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Socket socket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socket);
        }

        // GET: Sockets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socket = await _context.Sockets.FindAsync(id);
            if (socket == null)
            {
                return NotFound();
            }
            return View(socket);
        }

        // POST: Sockets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Socket socket)
        {
            if (id != socket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocketExists(socket.Id))
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
            return View(socket);
        }

        // GET: Sockets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socket = await _context.Sockets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socket == null)
            {
                return NotFound();
            }

            return View(socket);
        }

        // POST: Sockets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socket = await _context.Sockets.FindAsync(id);
            _context.Sockets.Remove(socket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocketExists(int id)
        {
            return _context.Sockets.Any(e => e.Id == id);
        }
    }
}
