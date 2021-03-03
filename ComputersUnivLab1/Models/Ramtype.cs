using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Ramtype
    {
        public Ramtype()
        {
            Motherboards = new HashSet<Motherboard>();
            Rams = new HashSet<Ram>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }
        public virtual ICollection<Ram> Rams { get; set; }
    }
}
