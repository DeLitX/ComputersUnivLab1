using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class RamsToComputer
    {
        public int Id { get; set; }
        public int Ramid { get; set; }
        public int ComputerId { get; set; }

        public virtual Computer Computer { get; set; }
        public virtual Ram Ram { get; set; }
    }
}
