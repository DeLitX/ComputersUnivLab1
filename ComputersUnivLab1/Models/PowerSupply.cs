using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class PowerSupply
    {
        public PowerSupply()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Power { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Thickness { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
    }
}
