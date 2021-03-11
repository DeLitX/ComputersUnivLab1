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
    public class RamsController : Controller
    {
        private readonly ComputersContext _context;

        public RamsController(ComputersContext context)
        {
            _context = context;
        }

        // GET: Rams
        public async Task<IActionResult> Index()
        {
            var computersContext = _context.Rams.Include(r => r.Type);
            return View(await computersContext.ToListAsync());
        }

        // GET: Rams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _context.Rams
                .Include(r => r.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }

        // GET: Rams/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.Ramtypes, "Id", "Name");
            return View();
        }

        // POST: Rams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Size,TypeId,Manufacturer,Speed,Price")] Ram ram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.Ramtypes, "Id", "Name", ram.TypeId);
            return View(ram);
        }

        // GET: Rams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _context.Rams.FindAsync(id);
            if (ram == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.Ramtypes, "Id", "Name", ram.TypeId);
            return View(ram);
        }

        // POST: Rams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Size,TypeId,Manufacturer,Speed,Price")] Ram ram)
        {
            if (id != ram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RamExists(ram.Id))
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
            ViewData["TypeId"] = new SelectList(_context.Ramtypes, "Id", "Name", ram.TypeId);
            return View(ram);
        }

        // GET: Rams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _context.Rams
                .Include(r => r.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }

        // POST: Rams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ram = await _context.Rams.FindAsync(id);
            _context.Rams.Remove(ram);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RamExists(int id)
        {
            return _context.Rams.Any(e => e.Id == id);
        }
    }
}
