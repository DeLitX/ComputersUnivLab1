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
    public class RamtypesController : Controller
    {
        private readonly ComputersContext _context;

        public RamtypesController(ComputersContext context)
        {
            _context = context;
        }

        // GET: Ramtypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ramtypes.ToListAsync());
        }

        // GET: Ramtypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ramtype = await _context.Ramtypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ramtype == null)
            {
                return NotFound();
            }

            return View(ramtype);
        }

        // GET: Ramtypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ramtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Ramtype ramtype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ramtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ramtype);
        }

        // GET: Ramtypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ramtype = await _context.Ramtypes.FindAsync(id);
            if (ramtype == null)
            {
                return NotFound();
            }
            return View(ramtype);
        }

        // POST: Ramtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Ramtype ramtype)
        {
            if (id != ramtype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ramtype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RamtypeExists(ramtype.Id))
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
            return View(ramtype);
        }

        // GET: Ramtypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ramtype = await _context.Ramtypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ramtype == null)
            {
                return NotFound();
            }

            return View(ramtype);
        }

        // POST: Ramtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ramtype = await _context.Ramtypes.FindAsync(id);
            _context.Ramtypes.Remove(ramtype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RamtypeExists(int id)
        {
            return _context.Ramtypes.Any(e => e.Id == id);
        }
    }
}
