using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Motherboard
    {
        public Motherboard()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int GpuinterfaceId { get; set; }
        public int SocketId { get; set; }
        public string Formfactor { get; set; }
        public int RamtypeId { get; set; }
        public int Ramcount { get; set; }
        public int Usbcount { get; set; }
        public int Price { get; set; }

        public virtual Gpuinterface Gpuinterface { get; set; }
        public virtual Ramtype Ramtype { get; set; }
        public virtual Socket Socket { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
