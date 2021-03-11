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
    public class GpuinterfacesController : Controller
    {
        private readonly ComputersContext _context;

        public GpuinterfacesController(ComputersContext context)
        {
            _context = context;
        }

        // GET: Gpuinterfaces
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gpuinterfaces.ToListAsync());
        }

        // GET: Gpuinterfaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpuinterface = await _context.Gpuinterfaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpuinterface == null)
            {
                return NotFound();
            }

            return View(gpuinterface);
        }

        // GET: Gpuinterfaces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gpuinterfaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Gpuinterface gpuinterface)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gpuinterface);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gpuinterface);
        }

        // GET: Gpuinterfaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpuinterface = await _context.Gpuinterfaces.FindAsync(id);
            if (gpuinterface == null)
            {
                return NotFound();
            }
            return View(gpuinterface);
        }

        // POST: Gpuinterfaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Gpuinterface gpuinterface)
        {
            if (id != gpuinterface.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gpuinterface);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GpuinterfaceExists(gpuinterface.Id))
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
            return View(gpuinterface);
        }

        // GET: Gpuinterfaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpuinterface = await _context.Gpuinterfaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpuinterface == null)
            {
                return NotFound();
            }

            return View(gpuinterface);
        }

        // POST: Gpuinterfaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gpuinterface = await _context.Gpuinterfaces.FindAsync(id);
            _context.Gpuinterfaces.Remove(gpuinterface);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GpuinterfaceExists(int id)
        {
            return _context.Gpuinterfaces.Any(e => e.Id == id);
        }
    }
}
