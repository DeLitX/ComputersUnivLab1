using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ComputersUnivLab1
{
    [Display(Name = "Материнська плата")]
    public partial class Motherboard
    {
        public Motherboard()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Name { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int GpuinterfaceId { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int SocketId { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Formfactor { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int RamtypeId { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Ramcount { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Usbcount { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Price { get; set; }

        public virtual Gpuinterface Gpuinterface { get; set; }
        public virtual Ramtype Ramtype { get; set; }
        public virtual Socket Socket { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
