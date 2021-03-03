using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class ComputersToDrive
    {
        public int Id { get; set; }
        public int DriveId { get; set; }
        public int ComputerId { get; set; }

        public virtual Computer Computer { get; set; }
        public virtual Drive Drive { get; set; }
    }
}
