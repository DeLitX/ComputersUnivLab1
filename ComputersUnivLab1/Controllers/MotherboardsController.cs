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
    public class MotherboardsController : Controller
    {
        private readonly ComputersContext _context;

        public MotherboardsController(ComputersContext context)
        {
            _context = context;
        }

        // GET: Motherboards
        public async Task<IActionResult> Index()
        {
            var computersContext = _context.Motherboards.Include(m => m.Gpuinterface).Include(m => m.Ramtype).Include(m => m.Socket);
            return View(await computersContext.ToListAsync());
        }

        // GET: Motherboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = await _context.Motherboards
                .Include(m => m.Gpuinterface)
                .Include(m => m.Ramtype)
                .Include(m => m.Socket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motherboard == null)
            {
                return NotFound();
            }

            return View(motherboard);
        }

        // GET: Motherboards/Create
        public IActionResult Create()
        {
            ViewData["GpuinterfaceId"] = new SelectList(_context.Gpuinterfaces, "Id", "Name");
            ViewData["RamtypeId"] = new SelectList(_context.Ramtypes, "Id", "Name");
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name");
            return View();
        }

        // POST: Motherboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GpuinterfaceId,SocketId,Formfactor,RamtypeId,Ramcount,Usbcount,Price")] Motherboard motherboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motherboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GpuinterfaceId"] = new SelectList(_context.Gpuinterfaces, "Id", "Id", motherboard.GpuinterfaceId);
            ViewData["RamtypeId"] = new SelectList(_context.Ramtypes, "Id", "Name", motherboard.RamtypeId);
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name", motherboard.SocketId);
            return View(motherboard);
        }

        // GET: Motherboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = await _context.Motherboards.FindAsync(id);
            if (motherboard == null)
            {
                return NotFound();
            }
            ViewData["GpuinterfaceId"] = new SelectList(_context.Gpuinterfaces, "Id", "Id", motherboard.GpuinterfaceId);
            ViewData["RamtypeId"] = new SelectList(_context.Ramtypes, "Id", "Name", motherboard.RamtypeId);
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name", motherboard.SocketId);
            return View(motherboard);
        }

        // POST: Motherboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GpuinterfaceId,SocketId,Formfactor,RamtypeId,Ramcount,Usbcount,Price")] Motherboard motherboard)
        {
            if (id != motherboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motherboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotherboardExists(motherboard.Id))
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
            ViewData["GpuinterfaceId"] = new SelectList(_context.Gpuinterfaces, "Id", "Id", motherboard.GpuinterfaceId);
            ViewData["RamtypeId"] = new SelectList(_context.Ramtypes, "Id", "Name", motherboard.RamtypeId);
            ViewData["SocketId"] = new SelectList(_context.Sockets, "Id", "Name", motherboard.SocketId);
            return View(motherboard);
        }

        // GET: Motherboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = await _context.Motherboards
                .Include(m => m.Gpuinterface)
                .Include(m => m.Ramtype)
                .Include(m => m.Socket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motherboard == null)
            {
                return NotFound();
            }

            return View(motherboard);
        }

        // POST: Motherboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motherboard = await _context.Motherboards.FindAsync(id);
            _context.Motherboards.Remove(motherboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotherboardExists(int id)
        {
            return _context.Motherboards.Any(e => e.Id == id);
        }
    }
}
