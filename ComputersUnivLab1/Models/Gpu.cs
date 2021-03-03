using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Gpu
    {
        public Gpu()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CoreNumber { get; set; }
        public double Clock { get; set; }
        public int MemorySize { get; set; }
        public int MemorySpeed { get; set; }
        public int Price { get; set; }
        public int InterfaceId { get; set; }

        public virtual Gpuinterface Interface { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
