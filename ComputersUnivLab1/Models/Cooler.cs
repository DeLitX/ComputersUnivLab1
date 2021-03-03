using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Cooler
    {
        public Cooler()
        {
            Computers = new HashSet<Computer>();
            SocketsToCoolers = new HashSet<SocketsToCooler>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Tdp { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
        public virtual ICollection<SocketsToCooler> SocketsToCoolers { get; set; }
    }
}
