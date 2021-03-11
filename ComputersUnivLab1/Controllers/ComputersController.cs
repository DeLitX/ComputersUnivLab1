using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputersUnivLab1;
using ComputersUnivLab1.Models;

namespace ComputersUnivLab1.Controllers
{
    public class ComputersController : Controller
    {
        private readonly ComputersContext _context;

        public ComputersController(ComputersContext context)
        {
            _context = context;
        }
        public JsonResult FilterMotherboards(int cpu, int gpu)
        {
            List<Motherboard> ByCpu = new List<Motherboard>();
            List<Motherboard> ByGpu = new List<Motherboard>();
            var motherboards = _context.Motherboards.ToList();
            var cpus = _context.Cpus.ToList();
            var gpus = _context.Gpus.ToList();
            try
            {
                ByCpu = _context.Motherboards.ToList().Where(z => z.SocketId == _context.Cpus.ToList().Find(a=>a.Id==cpu).SocketId).ToList();
            } catch (Exception e)
            {
                 ByCpu = motherboards;
            }

            try {
                ByGpu = _context.Motherboards.Where(z => z.GpuinterfaceId == _context.Gpus.Where(a => a.Id == gpu).ToList()[0].InterfaceId).ToList();
            } catch (Exception e)
            {
                ByGpu = _context.Motherboards.ToList();
            }
            var result = ByCpu.Intersect(ByGpu).ToList();
            return Json(new SelectList(result, "Id", "Name"));
        }
        public JsonResult FilterCoolers(int? cpu)
        {
            List<Cooler> result = new List<Cooler>();
            try
            {
                result = _context.Coolers.Where(z => z.SocketsToCoolers.Where(a => (a.CoolerId == z.Id) && (a.SocketId == _context.Cpus.Where(b => b.Id == cpu).ToList()[0].SocketId)).ToList().Count != 0).ToList();
            }
            catch (Exception e)
            {
                result = _context.Coolers.ToList();
            }
            return Json(new SelectList(result, "Id", "Name"));
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
            ViewData["Rams"] = _context.Rams.ToList();
            ViewData["Drives"] = _context.Drives.ToList();
            var computersContext = _context.Computers.Include(c => c.Cooler).Include(c => c.Cpu).Include(c => c.Gpu).Include(c => c.Motherboard).Include(c => c.PowerSupply).Include(c=>c.RamsToComputers).Include(c=>c.ComputersToDrives);
            return View(await computersContext.ToListAsync());
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers
                .Include(c => c.Cooler)
                .Include(c => c.Cpu)
                .Include(c => c.Gpu)
                .Include(c => c.Motherboard)
                .Include(c => c.PowerSupply)
                .Include(c => c.RamsToComputers)
                .Include(c => c.ComputersToDrives)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computer == null)
            {
                return NotFound();
            }
            var allRams = _context.Rams.ToList();
            var allDrives = _context.Drives.ToList();
            var rams = allRams.Where(c => computer.RamsToComputers.Where(a => a.Ramid == c.Id).Count() != 0).ToList();
            var drives = allDrives.Where(c => computer.ComputersToDrives.Where(a => a.DriveId == c.Id).Count() != 0).ToList();
            string text = "";
            bool isFirst = true;
            for(int i = 0; i < rams.Count(); i++)
            {
                if (!isFirst)
                {
                    text += "   |   ";
                }
                text += rams[i].Name;
                isFirst = false;
            }
            ViewData["Rams"] = text;
            text = "";
            isFirst = true;
            for (int i = 0; i < drives.Count(); i++)
            {
                if (!isFirst)
                {
                    text += "   |   ";
                }
                text += drives[i].Name;
                isFirst = false;
            }
            ViewData["Drives"] = text;

            return View(computer);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            ViewData["CoolerId"] = new SelectList(_context.Coolers, "Id", "Name");
            ViewData["Cpuid"] = new SelectList(_context.Cpus, "Id", "Name");
            ViewData["Gpuid"] = new SelectList(_context.Gpus, "Id", "Name");
            ViewData["MotherboardId"] = new SelectList(_context.Motherboards, "Id", "Name");
            ViewData["PowerSupplyId"] = new SelectList(_context.PowerSupplies, "Id", "Name");
            ViewData["RamId"] = new SelectList(_context.Rams, "Id", "Name");
            ViewData["DriveId"] = new SelectList(_context.Drives, "Id", "Name");
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Computer computer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computer);
                await _context.SaveChangesAsync();

                List<ComputersToDrive> drives = new List<ComputersToDrive>();
                foreach (var i in computer.SelectedRam)
                {
                    _context.Add(new RamsToComputer()
                    {
                        ComputerId = computer.Id,
                        Ramid = i
                    });
                }
                List<RamsToComputer> rams = new List<RamsToComputer>();
                foreach (var i in computer.SelectedDrive)
                {
                    _context.Add(new ComputersToDrive()
                    {
                        ComputerId = computer.Id,
                        DriveId = i
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoolerId"] = new SelectList(_context.Coolers, "Id", "Name", computer.CoolerId);
            ViewData["Cpuid"] = new SelectList(_context.Cpus, "Id", "Name", computer.Cpuid);
            ViewData["Gpuid"] = new SelectList(_context.Gpus, "Id", "Name", computer.Gpuid);
            ViewData["MotherboardId"] = new SelectList(_context.Motherboards, "Id", "Name", computer.MotherboardId);
            ViewData["PowerSupplyId"] = new SelectList(_context.PowerSupplies, "Id", "Name", computer.PowerSupplyId);
            ViewData["RamId"] = new SelectList(_context.Rams, "Id", "Name",computer.RamsToComputers);
            ViewData["DriveId"] = new SelectList(_context.Drives, "Id", "Name",computer.ComputersToDrives);
            return View(computer);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            ViewData["CoolerId"] = new SelectList(_context.Coolers, "Id", "Name", computer.CoolerId);
            ViewData["Cpuid"] = new SelectList(_context.Cpus, "Id", "Name", computer.Cpuid);
            ViewData["Gpuid"] = new SelectList(_context.Gpus, "Id", "Name", computer.Gpuid);
            ViewData["MotherboardId"] = new SelectList(_context.Motherboards, "Id", "Name", computer.MotherboardId);
            ViewData["PowerSupplyId"] = new SelectList(_context.PowerSupplies, "Id", "Name", computer.PowerSupplyId);
            ViewData["RamId"] = new SelectList(_context.Rams, "Id", "Name",computer.RamsToComputers);
            ViewData["DriveId"] = new SelectList(_context.Drives, "Id", "Name",computer.ComputersToDrives);
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Computer computer)
        {
            if (id != computer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    List<ComputersToDrive> drives = _context.ComputersToDrives.ToList().Where(a=>a.ComputerId==id).ToList();
                    foreach (var i in drives)
                    {
                         _context.Remove(i);
                    }
                    foreach (int i in computer.SelectedDrive)
                    {
                        _context.Add(new ComputersToDrive()
                        {
                            ComputerId = id,
                            DriveId = i
                        });
                    }
                    List<RamsToComputer> rams = _context.RamsToComputers.ToList().Where(a => a.ComputerId == id).ToList();
                    foreach (var i in rams)
                    {
                        _context.Remove(i);
                    }
                    foreach (int i in computer.SelectedRam)
                    {
                        _context.Add(new RamsToComputer()
                        {
                            ComputerId = id,
                            Ramid = i
                        });
                    }
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.Id))
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
            ViewData["CoolerId"] = new SelectList(_context.Coolers, "Id", "Name", computer.CoolerId);
            ViewData["Cpuid"] = new SelectList(_context.Cpus, "Id", "Name", computer.Cpuid);
            ViewData["Gpuid"] = new SelectList(_context.Gpus, "Id", "Name", computer.Gpuid);
            ViewData["MotherboardId"] = new SelectList(_context.Motherboards, "Id", "Name", computer.MotherboardId);
            ViewData["PowerSupplyId"] = new SelectList(_context.PowerSupplies, "Id", "Name", computer.PowerSupplyId);
            ViewData["RamId"] = new SelectList(_context.Rams, "Id", "Name",computer.RamsToComputers);
            ViewData["DriveId"] = new SelectList(_context.Drives, "Id", "Name",computer.ComputersToDrives);
            return View(computer);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computers
                .Include(c => c.Cooler)
                .Include(c => c.Cpu)
                .Include(c => c.Gpu)
                .Include(c => c.Motherboard)
                .Include(c => c.PowerSupply)
                .Include(c=>c.RamsToComputers)
                .Include(c=>c.ComputersToDrives)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            var drives = _context.ComputersToDrives.ToList();
            var rams = _context.RamsToComputers.ToList();
            foreach (var i in drives)
            {
                if (i.ComputerId == id)
                {
                    _context.ComputersToDrives.Remove(i);
                }
            }
            foreach (var i in rams)
            {
                if (i.ComputerId == id)
                {
                    _context.RamsToComputers.Remove(i);
                }
            }
            await _context.SaveChangesAsync();
            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerExists(int id)
        {
            return _context.Computers.Any(e => e.Id == id);
        }
    }
}
