using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ComputersUnivLab1
{
    [Display(Name = "Кулер")]
    public partial class Cooler
    {
        public Cooler()
        {
            Computers = new HashSet<Computer>();
            SocketsToCoolers = new HashSet<SocketsToCooler>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Name { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Weight { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Tdp { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Price { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
        public virtual ICollection<SocketsToCooler> SocketsToCoolers { get; set; }
    }
}
