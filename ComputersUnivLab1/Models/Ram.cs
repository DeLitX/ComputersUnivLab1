using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Ram
    {
        public Ram()
        {
            RamsToComputers = new HashSet<RamsToComputer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int TypeId { get; set; }
        public string Manufacturer { get; set; }
        public int Speed { get; set; }
        public int Price { get; set; }

        public virtual Ramtype Type { get; set; }
        public virtual ICollection<RamsToComputer> RamsToComputers { get; set; }
    }
}
