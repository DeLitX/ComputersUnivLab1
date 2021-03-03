using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Gpuinterface
    {
        public Gpuinterface()
        {
            Gpus = new HashSet<Gpu>();
            Motherboards = new HashSet<Motherboard>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Gpu> Gpus { get; set; }
        public virtual ICollection<Motherboard> Motherboards { get; set; }
    }
}
