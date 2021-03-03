using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Cpu
    {
        public Cpu()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CoresNumber { get; set; }
        public int ThreadsNumber { get; set; }
        public double Clock { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public int SocketId { get; set; }

        public virtual Socket Socket { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
