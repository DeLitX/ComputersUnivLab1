using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputersUnivLab1;
using ComputersUnivLab1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;

namespace ComputersUnivLab1.Controllers
{
    public class ComputersController : Controller
    {
        private readonly ComputersContext _context;

        public ComputersController(ComputersContext context)
        {
            _context = context;
        }
        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var parser = new ExcelParser(_context);
                parser.addComputersToExcel(workbook);
                /*parser.addCoolersToExcel(workbook);
                parser.addCpusToExcel(workbook);
                parser.addDrivesToExcel(workbook);
                parser.addGpuInterfacesToExcel(workbook);
                parser.addGpusToExcel(workbook);
                parser.addMotherboardsToExcel(workbook);
                parser.addPowerSuppliesToExcel(workbook);
                parser.addRamsToExcel(workbook);
                parser.addRamTypesToExcel(workbook);
                parser.addSocketsToExcel(workbook);*/
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"computers_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            var parser = new ExcelParser(_context);
                            var computers = parser.getComputersFromExcel(workBook);
                            /*var coolers = parser.getCoolersFromExcel(workBook);
                            var cpus = parser.getCpusFromExcel(workBook);
                            var drives = parser.getDrivesFromExcel(workBook);
                            var gpuInterfaces = parser.getGpuInterfacesFromExcel(workBook);
                            var gpu = parser.getGpusFromExcel(workBook);
                            var motherboards = parser.getMotherboardsFromExcel(workBook);
                            var powerSupplies = parser.getPowerSuppliesFromExcel(workBook);
                            var rams = parser.getRamsFromExcel(workBook);
                            var ramTypes = parser.getRamTypesFromExcel(workBook);
                            var sockets = parser.getSocketsFromExcel(workBook);*/
                            
                            /*List<SocketsToCooler> socketsToCoolers = new List<SocketsToCooler>();
                            for (int i = 0; i < coolers.Count(); i++)
                            {
                                for (int k = 0; k < coolers.ElementAt(i).SocketsToCoolers.Count(); k++)
                                {
                                    var temp = new SocketsToCooler
                                    {
                                        CoolerId = coolers.ElementAt(i).Id,
                                        SocketId = coolers.ElementAt(i).SocketsToCoolers.ElementAt(k).SocketId
                                    };
                                    coolers.ElementAt(i).SocketsToCoolers.Add(temp);
                                    socketsToCoolers.Add(temp);
                                }
                            }*/
                            //replaceListInDB(socketsToCoolers, _context.SocketsToCoolers.ToList(),new SocketsToCoolerComparer());
                            insertComputers(computers.ToList());
                            //replaceListInDB(coolers, _context.Coolers.ToList());
                            //replaceListInDB(cpus,  _context.Cpus.ToList());
                            //replaceListInDB(drives, _context.Drives.ToList());
                            //replaceListInDB(gpuInterfaces,  _context.Gpuinterfaces.ToList());
                            //replaceListInDB(gpu,  _context.Gpus.ToList());
                            //replaceListInDB(motherboards, _context.Motherboards.ToList());
                            //replaceListInDB(powerSupplies,  _context.PowerSupplies.ToList());
                            //replaceListInDB(rams,  _context.Rams.ToList());
                            //replaceListInDB(ramTypes, _context.Ramtypes.ToList());
                            //replaceListInDB(sockets,  _context.Sockets.ToList());
                            //setIdentityInsert();
                            await _context.SaveChangesAsync();
                            List<RamsToComputer> ramToComputers = new List<RamsToComputer>();
                            for (int i = 0; i < computers.Count(); i++)
                            {
                                for (int k = 0; k < computers.ElementAt(i).SelectedRam.Count(); k++)
                                {
                                    var temp = new RamsToComputer
                                    {
                                        ComputerId = computers.ElementAt(i).Id,
                                        Ramid = computers.ElementAt(i).SelectedRam[k]
                                    };
                                    computers.ElementAt(i).RamsToComputers.Add(temp);
                                    _context.Add(temp);
                                }
                            }
                            List<ComputersToDrive> drivesToComputers = new List<ComputersToDrive>();
                            for (int i = 0; i < computers.Count(); i++)
                            {
                                for (int k = 0; k < computers.ElementAt(i).SelectedDrive.Count(); k++)
                                {
                                    var temp = new ComputersToDrive
                                    {
                                        ComputerId = computers.ElementAt(i).Id,
                                        DriveId = computers.ElementAt(i).SelectedDrive[k]
                                    };
                                    computers.ElementAt(i).ComputersToDrives.Add(temp);
                                    _context.Add(temp);
                                }
                            }

                            //replaceListInDB(ramToComputers, _context.RamsToComputers.ToList(),new RamsToComputerComparer());
                            //replaceListInDB(drivesToComputers, _context.ComputersToDrives.ToList(),new ComputersToDriveComparer());
                            await _context.SaveChangesAsync();
                            //setIdentityNotInsert();
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        void insertComputers(List<Computer> computers)
        {
            foreach (var i in computers)
            {
                _context.Add(i);
                _context.SaveChanges();
            }
        }
        void setIdentityInsert()
        {
            _context.Database.ExecuteSqlRaw(
             "SET IDENTITY_INSERT dbo.CPUs ON \n"
            /*+ "SET IDENTITY_INSERT dbo.Drives ON \n"
            + "SET IDENTITY_INSERT dbo.GPUInterfaces ON \n"
            + "SET IDENTITY_INSERT dbo.GPUs ON \n"*/
           // + "SET IDENTITY_INSERT dbo.Motherboards ON \n"
            //+ "SET IDENTITY_INSERT dbo.PowerSupplies ON \n "
            //+ "SET IDENTITY_INSERT dbo.RAMs ON \n"
            /*+ "SET IDENTITY_INSERT dbo.RAMsToComputers ON \n"
            + "SET IDENTITY_INSERT dbo.RAMTypes ON \n"
            + "SET IDENTITY_INSERT dbo.Sockets ON \n"
            + "SET IDENTITY_INSERT dbo.SocketsToCoolers ON \n"*/);
        }
        void setIdentityNotInsert()
        {
            _context.Database.ExecuteSqlRaw(
              "SET IDENTITY_INSERT dbo.CPUs OFF \n"
           /* + "SET IDENTITY_INSERT dbo.Drives OFF \n"
            + "SET IDENTITY_INSERT dbo.GPUInterfaces OFF \n"
            + "SET IDENTITY_INSERT dbo.GPUs OFF \n"*/
           // + "SET IDENTITY_INSERT dbo.Motherboards OFF \n"
           // + "SET IDENTITY_INSERT dbo.PowerSupplies OFF \n"
            //+ "SET IDENTITY_INSERT dbo.RAMs OFF \n"
           /* + "SET IDENTITY_INSERT dbo.RAMsToComputers OFF \n"
            + "SET IDENTITY_INSERT dbo.RAMTypes OFF \n"
            + "SET IDENTITY_INSERT dbo.Sockets OFF \n"
            + "SET IDENTITY_INSERT dbo.SocketsToCoolers OFF \n"*/);
        }
        void replaceListInDB<T>(ICollection<T> listToInsert, ICollection<T> listToReplace)
        {
            var substraction = listToReplace.Except(listToInsert).ToList();
            /*foreach (var i in substraction)
            {
                _context.Remove(i);
            }*/
            foreach (var i in listToInsert)
            {
                try
                {
                    _context.Add(i);
                }
                catch(InvalidOperationException e)
                {
                    _context.Update(i);
                }
            }
        }
        void replaceListInDB<T>(ICollection<T>listToInsert,ICollection<T> listToReplace, IEqualityComparer<T> comparer)
        {
            var substraction = listToReplace.Except(listToInsert,comparer).ToList();
            foreach(var i in substraction)
            {
                _context.Remove(i);
            }
            foreach(var i in listToInsert)
            {
                /*try
                {*/
                    _context.Add(i);
                /*}
                catch (InvalidOperationException e)
                {
                    _context.Update(i);
                }*/
            }
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
