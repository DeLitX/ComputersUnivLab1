﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class Computer
    {
        public Computer()
        {
            ComputersToDrives = new HashSet<ComputersToDrive>();
            RamsToComputers = new HashSet<RamsToComputer>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name ="Процесор")]
        public int Cpuid { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "Відеокарта")]
        public int Gpuid { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "Материнська плата")]
        public int MotherboardId { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "Кулер")]
        public int CoolerId { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "Блок живлення")]
        public int PowerSupplyId { get; set; }

        public virtual Cooler Cooler { get; set; }
        public virtual Cpu Cpu { get; set; }
        public virtual Gpu Gpu { get; set; }
        public virtual Motherboard Motherboard { get; set; }
        public virtual PowerSupply PowerSupply { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "Диски")]
        public virtual ICollection<ComputersToDrive> ComputersToDrives { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "Диски")]
        [NotMapped]
        public int[] SelectedDrive { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "ОЗП")]
        public virtual ICollection<RamsToComputer> RamsToComputers { get; set; }
        [Required(ErrorMessage = Constants.FIELD_MUST_NOT_BE_NULL)]
        [Display(Name = "ОЗП")]
        [NotMapped]
        public int[] SelectedRam { get; set; }
    }
    public class ComputerComparer : IEqualityComparer<Computer>
    {
        bool IEqualityComparer<Computer>.Equals(Computer x, Computer y)
        {
            return x.Id == y.Id &&
                x.Gpuid == y.Gpuid &&
                x.Cpuid == y.Cpuid &&
                x.MotherboardId == y.MotherboardId &&
                x.CoolerId == y.CoolerId &&
                x.PowerSupplyId == y.PowerSupplyId &&
                x.ComputersToDrives == y.ComputersToDrives &&
                x.RamsToComputers == y.RamsToComputers;
        }

        int IEqualityComparer<Computer>.GetHashCode(Computer obj)
        {
            return obj.Id;
        }
    }
}
