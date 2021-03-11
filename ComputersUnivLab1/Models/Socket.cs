using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Socket
    {
        public Socket()
        {
            Cpus = new HashSet<Cpu>();
            Motherboards = new HashSet<Motherboard>();
            SocketsToCoolers = new HashSet<SocketsToCooler>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Name { get; set; }

        public virtual ICollection<Cpu> Cpus { get; set; }
        public virtual ICollection<Motherboard> Motherboards { get; set; }
        public virtual ICollection<SocketsToCooler> SocketsToCoolers { get; set; }
    }
}
