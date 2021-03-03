using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Drive
    {
        public Drive()
        {
            ComputersToDrives = new HashSet<ComputersToDrive>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }
        public string Interface { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Thickness { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }

        public virtual ICollection<ComputersToDrive> ComputersToDrives { get; set; }
    }
}
