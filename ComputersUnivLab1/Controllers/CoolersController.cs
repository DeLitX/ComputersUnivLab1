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
    public class CoolersController : Controller
    {
        private readonly ComputersContext _context;

        public CoolersController(ComputersContext context)
        {
            _context = context;
        }

        // GET: Coolers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coolers.ToListAsync());
        }

        // GET: Coolers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cooler = await _context.Coolers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cooler == null)
            {
                return NotFound();
            }

            return View(cooler);
        }

        // GET: Coolers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coolers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Weight,Tdp,Price")] Cooler cooler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cooler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cooler);
        }

        // GET: Coolers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cooler = await _context.Coolers.FindAsync(id);
            if (cooler == null)
            {
                return NotFound();
            }
            return View(cooler);
        }

        // POST: Coolers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Weight,Tdp,Price")] Cooler cooler)
        {
            if (id != cooler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cooler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoolerExists(cooler.Id))
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
            return View(cooler);
        }

        // GET: Coolers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cooler = await _context.Coolers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cooler == null)
            {
                return NotFound();
            }

            return View(cooler);
        }

        // POST: Coolers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cooler = await _context.Coolers.FindAsync(id);
            _context.Coolers.Remove(cooler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoolerExists(int id)
        {
            return _context.Coolers.Any(e => e.Id == id);
        }
    }
}
