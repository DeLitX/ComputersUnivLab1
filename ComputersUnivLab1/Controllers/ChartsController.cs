using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputersUnivLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly ComputersContext _context;
        public ChartsController(ComputersContext context)
        {
            _context = context;
        }
        [HttpGet("CpuData")]
        public JsonResult CpuData()
        {
            var computers = _context.Computers.Include(a => a.Cpu).ToList();
            List<object> cpus = new List<object>();
            cpus.Add(new[] { "Назва", "Кількість" });
            Dictionary<string, int> cpuCount = new Dictionary<string, int>();
            foreach (var c in computers)
            {
                try
                {
                    cpuCount[c.Cpu.Name]++;
                }
                catch (KeyNotFoundException e)
                {
                    cpuCount.Add(c.Cpu.Name, 1);
                }
            }
            foreach (var a in cpuCount)
            {
                cpus.Add(new object[] { a.Key, a.Value });
            }
            return new JsonResult(cpus);
        }

        [HttpGet("GpuData")]
        public JsonResult GpuData()
        {
            var computers = _context.Computers.Include(a => a.Gpu).ToList();
            List<object> gpus = new List<object>();
            gpus.Add(new[] { "Назва", "Кількість" });
            Dictionary<string, int> gpuCount = new Dictionary<string, int>();
            foreach (var c in computers)
            {
                try
                {
                    gpuCount[c.Gpu.Name]++;
                }
                catch (KeyNotFoundException e)
                {
                    gpuCount.Add(c.Gpu.Name, 1);
                }
            }
            foreach (var a in gpuCount)
            {
                gpus.Add(new object[] { a.Key, a.Value });
            }
            return new JsonResult(gpus);
        }
    }
}
