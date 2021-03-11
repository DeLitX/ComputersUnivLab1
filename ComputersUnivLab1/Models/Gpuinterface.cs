using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Gpuinterface
    {
        public Gpuinterface()
        {
            Gpus = new HashSet<Gpu>();
            Motherboards = new HashSet<Motherboard>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Name { get; set; }

        public virtual ICollection<Gpu> Gpus { get; set; }
        public virtual ICollection<Motherboard> Motherboards { get; set; }
    }
}
