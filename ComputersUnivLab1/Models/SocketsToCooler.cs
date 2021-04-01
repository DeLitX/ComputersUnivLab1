using System;
using System.Collections.Generic;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class SocketsToCooler
    {
        public int Id { get; set; }
        public int SocketId { get; set; }
        public int CoolerId { get; set; }

        public virtual Cooler Cooler { get; set; }
        public virtual Socket Socket { get; set; }
        
    }
    public class SocketsToCoolerComparer : IEqualityComparer<SocketsToCooler>
    {
        bool IEqualityComparer<SocketsToCooler>.Equals(SocketsToCooler x, SocketsToCooler y)
        {
            return x.SocketId == y.SocketId && x.CoolerId == y.CoolerId;
        }

        int IEqualityComparer<SocketsToCooler>.GetHashCode(SocketsToCooler obj)
        {
            return obj.SocketId + obj.CoolerId;
        }
    }
}
