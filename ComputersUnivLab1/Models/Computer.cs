using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Computer
    {
        public Computer()
        {
            ComputersToDrives = new HashSet<ComputersToDrive>();
            RamsToComputers = new HashSet<RamsToComputer>();
        }

        public int Id { get; set; }
        public int Cpuid { get; set; }
        public int Gpuid { get; set; }
        public int MotherboardId { get; set; }
        public int CoolerId { get; set; }
        public int PowerSupplyId { get; set; }

        public virtual Cooler Cooler { get; set; }
        public virtual Cpu Cpu { get; set; }
        public virtual Gpu Gpu { get; set; }
        public virtual Motherboard Motherboard { get; set; }
        public virtual PowerSupply PowerSupply { get; set; }
        public virtual ICollection<ComputersToDrive> ComputersToDrives { get; set; }
        public virtual ICollection<RamsToComputer> RamsToComputers { get; set; }
    }
}
