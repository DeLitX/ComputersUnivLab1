using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ComputersUnivLab1
{
    [Display(Name = "Процесор")]
    public partial class Cpu
    {
        public Cpu()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Name { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int CoresNumber { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int ThreadsNumber { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public double Clock { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Price { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int SocketId { get; set; }

        public virtual Socket Socket { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
