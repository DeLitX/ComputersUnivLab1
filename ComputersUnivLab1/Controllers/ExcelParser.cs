using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputersUnivLab1.Controllers
{
    public class ExcelParser
    {
        private ComputersContext _context;
        public ExcelParser(ComputersContext context)
        {
            _context = context;
        }
        public void addComputersToExcel(XLWorkbook workbook)
        {

            var computers = _context.Computers.Include(a => a.RamsToComputers).Include(a => a.ComputersToDrives).ToList();
            var worksheet = workbook.Worksheets.Add("Комп'ютери");

            //worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Cpuid";
            worksheet.Cell("C1").Value = "Gpuid";
            worksheet.Cell("D1").Value = "MotherboardId";
            worksheet.Cell("E1").Value = "CoolerId";
            worksheet.Cell("F1").Value = "PowerSupplyId";
            worksheet.Cell("G1").Value = "SelectedDrive";
            worksheet.Cell("H1").Value = "SelectedRam";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < computers.Count; i++)
            {
                //worksheet.Cell(i + 2, 1).Value = computers[i].Id;
                worksheet.Cell(i + 2, 2).Value = computers[i].Cpuid;
                worksheet.Cell(i + 2, 3).Value = computers[i].Gpuid;
                worksheet.Cell(i + 2, 4).Value = computers[i].MotherboardId;
                worksheet.Cell(i + 2, 5).Value = computers[i].CoolerId;
                worksheet.Cell(i + 2, 6).Value = computers[i].PowerSupplyId;
                worksheet.Cell(i + 2, 7).Value = listToString(computers[i].ComputersToDrives, (item) => item.DriveId.ToString());
                worksheet.Cell(i + 2, 8).Value = listToString(computers[i].RamsToComputers, (item) => item.Ramid.ToString());
            }
        }
        public void addMotherboardsToExcel(XLWorkbook workbook)
        {

            var motherboards = _context.Motherboards.ToList();
            var worksheet = workbook.Worksheets.Add("Материнські плати");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Cell("C1").Value = "GpuinterfaceId";
            worksheet.Cell("D1").Value = "SocketId";
            worksheet.Cell("E1").Value = "Formfactor";
            worksheet.Cell("F1").Value = "RamtypeId";
            worksheet.Cell("G1").Value = "Ramcount";
            worksheet.Cell("H1").Value = "Usbcount";
            worksheet.Cell("I1").Value = "Price";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < motherboards.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = motherboards[i].Id;
                worksheet.Cell(i + 2, 2).Value = motherboards[i].Name;
                worksheet.Cell(i + 2, 3).Value = motherboards[i].GpuinterfaceId;
                worksheet.Cell(i + 2, 4).Value = motherboards[i].SocketId;
                worksheet.Cell(i + 2, 5).Value = motherboards[i].Formfactor;
                worksheet.Cell(i + 2, 6).Value = motherboards[i].RamtypeId;
                worksheet.Cell(i + 2, 7).Value = motherboards[i].Ramcount;
                worksheet.Cell(i + 2, 8).Value = motherboards[i].Usbcount;
                worksheet.Cell(i + 2, 9).Value = motherboards[i].Price;
            }
        }
        public void addCoolersToExcel(XLWorkbook workbook)
        {

            var coolers = _context.Coolers.Include(a => a.SocketsToCoolers).ToList();
            var worksheet = workbook.Worksheets.Add("Кулери");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Cell("C1").Value = "Weight";
            worksheet.Cell("D1").Value = "Tdp";
            worksheet.Cell("E1").Value = "Price";
            worksheet.Cell("F1").Value = "SocketsIds";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < coolers.Count; i++)
            {
                var item = coolers[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
                worksheet.Cell(i + 2, 3).Value = item.Weight;
                worksheet.Cell(i + 2, 4).Value = item.Tdp;
                worksheet.Cell(i + 2, 5).Value = item.Price;
                worksheet.Cell(i + 2, 6).Value = listToString(item.SocketsToCoolers, (socket) => socket.SocketId.ToString());
            }
        }
        public void addCpusToExcel(XLWorkbook workbook)
        {

            var cpus = _context.Cpus.ToList();
            var worksheet = workbook.Worksheets.Add("Процесори");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Cell("C1").Value = "CoresNumber";
            worksheet.Cell("D1").Value = "ThreadsNumber";
            worksheet.Cell("E1").Value = "Clock";
            worksheet.Cell("F1").Value = "Manufacturer";
            worksheet.Cell("G1").Value = "Price";
            worksheet.Cell("H1").Value = "SocketId";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < cpus.Count; i++)
            {
                var item = cpus[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
                worksheet.Cell(i + 2, 3).Value = item.CoresNumber;
                worksheet.Cell(i + 2, 4).Value = item.ThreadsNumber;
                worksheet.Cell(i + 2, 5).Value = item.Clock;
                worksheet.Cell(i + 2, 6).Value = item.Manufacturer;
                worksheet.Cell(i + 2, 7).Value = item.Price;
                worksheet.Cell(i + 2, 8).Value = item.SocketId;
            }
        }
        public void addDrivesToExcel(XLWorkbook workbook)
        {

            var drives = _context.Drives.ToList();
            var worksheet = workbook.Worksheets.Add("Диски");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Cell("C1").Value = "Size";
            worksheet.Cell("D1").Value = "ReadSpeed";
            worksheet.Cell("E1").Value = "WriteSpeed";
            worksheet.Cell("F1").Value = "Interface";
            worksheet.Cell("G1").Value = "Width";
            worksheet.Cell("H1").Value = "Height";
            worksheet.Cell("I1").Value = "Thickness";
            worksheet.Cell("J1").Value = "Manufacturer";
            worksheet.Cell("K1").Value = "Price";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < drives.Count; i++)
            {
                var item = drives[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
                worksheet.Cell(i + 2, 3).Value = item.Size;
                worksheet.Cell(i + 2, 4).Value = item.ReadSpeed;
                worksheet.Cell(i + 2, 5).Value = item.WriteSpeed;
                worksheet.Cell(i + 2, 6).Value = item.Interface;
                worksheet.Cell(i + 2, 7).Value = item.Width;
                worksheet.Cell(i + 2, 8).Value = item.Height;
                worksheet.Cell(i + 2, 9).Value = item.Thickness;
                worksheet.Cell(i + 2, 10).Value = item.Manufacturer;
                worksheet.Cell(i + 2, 11).Value = item.Price;
            }
        }
        public void addGpusToExcel(XLWorkbook workbook)
        {
            var gpus = _context.Gpus.ToList();
            var worksheet = workbook.Worksheets.Add("Відеокарти");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Cell("C1").Value = "CoreNumber";
            worksheet.Cell("D1").Value = "Clock";
            worksheet.Cell("E1").Value = "MemorySize";
            worksheet.Cell("F1").Value = "MemorySpeed";
            worksheet.Cell("G1").Value = "Price";
            worksheet.Cell("H1").Value = "InterfaceId";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < gpus.Count; i++)
            {
                var item = gpus[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
                worksheet.Cell(i + 2, 3).Value = item.CoreNumber;
                worksheet.Cell(i + 2, 4).Value = item.Clock;
                worksheet.Cell(i + 2, 5).Value = item.MemorySize;
                worksheet.Cell(i + 2, 6).Value = item.MemorySpeed;
                worksheet.Cell(i + 2, 7).Value = item.Price;
                worksheet.Cell(i + 2, 8).Value = item.InterfaceId;
            }
        }
        public void addGpuInterfacesToExcel(XLWorkbook workbook)
        {
            var gpuInterfaces = _context.Gpuinterfaces.ToList();
            var worksheet = workbook.Worksheets.Add("Інтерфейси відеокарт");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < gpuInterfaces.Count; i++)
            {
                var item = gpuInterfaces[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
            }
        }
        public void addPowerSuppliesToExcel(XLWorkbook workbook)
        {
            var powerSupplies = _context.PowerSupplies.ToList();
            var worksheet = workbook.Worksheets.Add("Блоки живлення");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Cell("C1").Value = "Manufacturer";
            worksheet.Cell("D1").Value = "Power";
            worksheet.Cell("E1").Value = "Width";
            worksheet.Cell("F1").Value = "Height";
            worksheet.Cell("G1").Value = "Thickness";
            worksheet.Cell("H1").Value = "Price";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < powerSupplies.Count; i++)
            {
                var item = powerSupplies[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
                worksheet.Cell(i + 2, 3).Value = item.Manufacturer;
                worksheet.Cell(i + 2, 4).Value = item.Power;
                worksheet.Cell(i + 2, 5).Value = item.Width;
                worksheet.Cell(i + 2, 6).Value = item.Height;
                worksheet.Cell(i + 2, 7).Value = item.Thickness;
                worksheet.Cell(i + 2, 8).Value = item.Price;
            }
        }
        public void addRamsToExcel(XLWorkbook workbook)
        {
            var rams = _context.Rams.ToList();
            var worksheet = workbook.Worksheets.Add("Оперативна пам'ять");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Cell("C1").Value = "Size";
            worksheet.Cell("D1").Value = "TypeId";
            worksheet.Cell("E1").Value = "Manufacturer";
            worksheet.Cell("F1").Value = "Speed";
            worksheet.Cell("G1").Value = "Price";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < rams.Count; i++)
            {
                var item = rams[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
                worksheet.Cell(i + 2, 3).Value = item.Size;
                worksheet.Cell(i + 2, 4).Value = item.TypeId;
                worksheet.Cell(i + 2, 5).Value = item.Manufacturer;
                worksheet.Cell(i + 2, 6).Value = item.Speed;
                worksheet.Cell(i + 2, 7).Value = item.Price;
            }
        }
        public void addRamTypesToExcel(XLWorkbook workbook)
        {
            var ramtypes = _context.Ramtypes.ToList();
            var worksheet = workbook.Worksheets.Add("Типи оперативної пам'яті");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < ramtypes.Count; i++)
            {
                var item = ramtypes[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
            }
        }
        public void addSocketsToExcel(XLWorkbook workbook)
        {
            var sockets = _context.Sockets.ToList();
            var worksheet = workbook.Worksheets.Add("Сокети");

            worksheet.Cell("A1").Value = "Id";
            worksheet.Cell("B1").Value = "Name";
            worksheet.Row(1).Style.Font.Bold = true;
            for (int i = 0; i < sockets.Count; i++)
            {
                var item = sockets[i];
                worksheet.Cell(i + 2, 1).Value = item.Id;
                worksheet.Cell(i + 2, 2).Value = item.Name;
            }
        }
        public ICollection<Computer> getComputersFromExcel(XLWorkbook workBook)
        {
            ICollection<Computer> result = new List<Computer>();
            var worksheet = workBook.Worksheet("Комп'ютери");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var computer = new Computer();
                //computer.Id = int.Parse(row.Cell(1).Value.ToString());
                computer.Cpuid = int.Parse(row.Cell(2).Value.ToString());
                computer.Gpuid = int.Parse(row.Cell(3).Value.ToString());
                computer.MotherboardId = int.Parse(row.Cell(4).Value.ToString());
                computer.CoolerId = int.Parse(row.Cell(5).Value.ToString());
                computer.PowerSupplyId = int.Parse(row.Cell(6).Value.ToString());
                computer.SelectedDrive = stringToIntList(row.Cell(7).Value.ToString()).ToArray();
                computer.SelectedRam = stringToIntList(row.Cell(8).Value.ToString()).ToArray();
                result.Add(computer);
            }
            return result;
        }
        public ICollection<Cooler> getCoolersFromExcel(XLWorkbook workBook)
        {
            ICollection<Cooler> result = new List<Cooler>();
            var worksheet = workBook.Worksheet("Кулери");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var cooler = new Cooler();
                //cooler.Id = int.Parse(row.Cell(1).Value.ToString());
                cooler.Name = row.Cell(2).Value.ToString();
                cooler.Weight = int.Parse(row.Cell(3).Value.ToString());
                cooler.Tdp = int.Parse(row.Cell(4).Value.ToString());
                cooler.Price = int.Parse(row.Cell(5).Value.ToString());
                var selectedSockets = stringToIntList(row.Cell(6).Value.ToString());
                List<SocketsToCooler> socketsToCoolers = new List<SocketsToCooler>();
                foreach(var i in selectedSockets)
                {
                    socketsToCoolers.Add(new SocketsToCooler
                    {
                        SocketId = i,
                        CoolerId = cooler.Id
                    });
                }
                result.Add(cooler);
            }
            return result;
        }
        public ICollection<Cpu> getCpusFromExcel(XLWorkbook workBook)
        {
            ICollection<Cpu> result = new List<Cpu>();
            var worksheet = workBook.Worksheet("Процесори");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var cpu = new Cpu();
                //cpu.Id = int.Parse(row.Cell(1).Value.ToString());
                cpu.Name = row.Cell(2).Value.ToString();
                cpu.CoresNumber = int.Parse(row.Cell(3).Value.ToString());
                cpu.ThreadsNumber = int.Parse(row.Cell(4).Value.ToString());
                cpu.Clock = double.Parse(row.Cell(5).Value.ToString());
                cpu.Manufacturer = row.Cell(6).Value.ToString();
                cpu.Price = int.Parse(row.Cell(7).Value.ToString());
                cpu.SocketId = int.Parse(row.Cell(8).Value.ToString());
                result.Add(cpu);
            }
            return result;
        }
        public ICollection<Drive> getDrivesFromExcel(XLWorkbook workBook)
        {
            ICollection<Drive> result = new List<Drive>();
            var worksheet = workBook.Worksheet("Диски");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new Drive();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                item.Size = int.Parse(row.Cell(3).Value.ToString());
                item.ReadSpeed = int.Parse(row.Cell(4).Value.ToString());
                item.WriteSpeed = int.Parse(row.Cell(5).Value.ToString());
                item.Interface = row.Cell(6).Value.ToString();
                item.Width = double.Parse(row.Cell(7).Value.ToString());
                item.Height = double.Parse(row.Cell(8).Value.ToString());
                item.Thickness = double.Parse(row.Cell(9).Value.ToString());
                item.Manufacturer = row.Cell(10).Value.ToString();
                item.Price = int.Parse(row.Cell(11).Value.ToString());
                result.Add(item);
            }
            return result;
        }
        public ICollection<Gpu> getGpusFromExcel(XLWorkbook workBook)
        {
            ICollection<Gpu> result = new List<Gpu>();
            var worksheet = workBook.Worksheet("Відеокарти");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new Gpu();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                item.CoreNumber = int.Parse(row.Cell(3).Value.ToString());
                item.Clock = int.Parse(row.Cell(4).Value.ToString());
                item.MemorySize = int.Parse(row.Cell(5).Value.ToString());
                item.MemorySpeed = int.Parse(row.Cell(6).Value.ToString());
                item.Price = int.Parse(row.Cell(7).Value.ToString());
                item.InterfaceId = int.Parse(row.Cell(8).Value.ToString());
                result.Add(item);
            }
            return result;
        }
        public ICollection<Gpuinterface> getGpuInterfacesFromExcel(XLWorkbook workBook)
        {
            ICollection<Gpuinterface> result = new List<Gpuinterface>();
            var worksheet = workBook.Worksheet("Інтерфейси відеокарт");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new Gpuinterface();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                result.Add(item);
            }
            return result;
        }
        public ICollection<Motherboard> getMotherboardsFromExcel(XLWorkbook workBook)
        {
            ICollection<Motherboard> result = new List<Motherboard>();
            var worksheet = workBook.Worksheet("Материнські плати");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new Motherboard();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                item.GpuinterfaceId = int.Parse(row.Cell(3).Value.ToString());
                item.SocketId = int.Parse(row.Cell(4).Value.ToString());
                item.Formfactor = row.Cell(5).Value.ToString();
                item.RamtypeId = int.Parse(row.Cell(6).Value.ToString());
                item.Ramcount = int.Parse(row.Cell(7).Value.ToString());
                item.Usbcount = int.Parse(row.Cell(8).Value.ToString());
                item.Price = int.Parse(row.Cell(9).Value.ToString());
                result.Add(item);
            }
            return result;
        }
        public ICollection<PowerSupply> getPowerSuppliesFromExcel(XLWorkbook workBook)
        {
            ICollection<PowerSupply> result = new List<PowerSupply>();
            var worksheet = workBook.Worksheet("Блоки живлення");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new PowerSupply();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                item.Manufacturer = row.Cell(3).Value.ToString();
                item.Power = int.Parse(row.Cell(4).Value.ToString());
                item.Width = double.Parse(row.Cell(5).Value.ToString());
                item.Height = double.Parse(row.Cell(6).Value.ToString());
                item.Thickness = double.Parse(row.Cell(7).Value.ToString());
                item.Price = int.Parse(row.Cell(8).Value.ToString());
                result.Add(item);
            }
            return result;
        }
        public ICollection<Ram> getRamsFromExcel(XLWorkbook workBook)
        {
            ICollection<Ram> result = new List<Ram>();
            var worksheet = workBook.Worksheet("Оперативна пам'ять");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new Ram();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                item.Size = int.Parse(row.Cell(3).Value.ToString());
                item.TypeId = int.Parse(row.Cell(4).Value.ToString());
                item.Manufacturer = row.Cell(5).Value.ToString();
                item.Speed = int.Parse(row.Cell(6).Value.ToString());
                item.Price = int.Parse(row.Cell(7).Value.ToString());
                result.Add(item);
            }
            return result;
        }
        public ICollection<Ramtype> getRamTypesFromExcel(XLWorkbook workBook)
        {
            ICollection<Ramtype> result = new List<Ramtype>();
            var worksheet = workBook.Worksheet("Типи оперативної пам'яті");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new Ramtype();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                result.Add(item);
            }
            return result;
        }
        public ICollection<Socket> getSocketsFromExcel(XLWorkbook workBook)
        {
            ICollection<Socket> result = new List<Socket>();
            var worksheet = workBook.Worksheet("Сокети");
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                var item = new Socket();
                //item.Id = int.Parse(row.Cell(1).Value.ToString());
                item.Name = row.Cell(2).Value.ToString();
                result.Add(item);
            }
            return result;
        }
        public ICollection<int> stringToIntList(string s)
        {
            var result = new List<int>();
            try
            {
                var temp = s.Split("|");
                foreach (var i in temp)
                {
                    result.Add(int.Parse(i));
                }
                return result;
            }catch(Exception e)
            {
                return new List<int>();
            }
        }
        string listToString<T>(ICollection<T> list, Func<T, string> action)
        {
            string result = "";
            bool isFirst = true;
            foreach (var i in list)
            {
                if (!isFirst)
                {
                    result += "|";
                }
                isFirst = false;
                result += action(i);
            }
            return result;
        }
    }
}
