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
    public class CpusController : Controller
    {
        private readonly ComputersContext _context;

        public CpusController(ComputersContext context)
        {
            _context = context;
        }

        // GET: Cpus
        public async Task<IActionResult> Index()
        {
            var computersContext = _context.Cpus.Include(c => c.Socket);
            return View(await computersContext.ToListAsync());
        }

        // GET: Cpus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpu = await _context.Cpus
                .Include(c => c.Socket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cpu == null)
            {
                return NotFound();
            }

            return View(cpu);
        }

        // GET: Cpus/Create
        public IActionResult Create()
        {
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name");
            return View();
        }

        // POST: Cpus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CoresNumber,ThreadsNumber,Clock,Manufacturer,Price,SocketId")] Cpu cpu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cpu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name", cpu.SocketId);
            return View(cpu);
        }

        // GET: Cpus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpu = await _context.Cpus.FindAsync(id);
            if (cpu == null)
            {
                return NotFound();
            }
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name", cpu.SocketId);
            return View(cpu);
        }

        // POST: Cpus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CoresNumber,ThreadsNumber,Clock,Manufacturer,Price,SocketId")] Cpu cpu)
        {
            if (id != cpu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cpu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CpuExists(cpu.Id))
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
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name", cpu.SocketId);
            return View(cpu);
        }

        // GET: Cpus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cpu = await _context.Cpus
                .Include(c => c.Socket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cpu == null)
            {
                return NotFound();
            }

            return View(cpu);
        }

        // POST: Cpus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cpu = await _context.Cpus.FindAsync(id);
            _context.Cpus.Remove(cpu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CpuExists(int id)
        {
            return _context.Cpus.Any(e => e.Id == id);
        }
    }
}
