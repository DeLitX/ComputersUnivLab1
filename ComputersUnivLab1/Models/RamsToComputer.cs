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
    public class RamsToComputerComparer : IEqualityComparer<RamsToComputer>
    {
        bool IEqualityComparer<RamsToComputer>.Equals(RamsToComputer x, RamsToComputer y)
        {
            return x.Ramid == y.Ramid && x.ComputerId == y.ComputerId;
        }

        int IEqualityComparer<RamsToComputer>.GetHashCode(RamsToComputer obj)
        {
            return obj.Ramid + obj.ComputerId;
        }
    }
}
