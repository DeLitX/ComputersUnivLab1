using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ComputersUnivLab1
{
    [Display(Name = "Блок живлення")]
    public partial class PowerSupply
    {
        public PowerSupply()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Name { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Power { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public double Width { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public double Height { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public double Thickness { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        public int Price { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
    }
}
