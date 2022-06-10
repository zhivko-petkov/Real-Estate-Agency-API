using Service.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEsWebMVC.ViewModels
{
    public class TownVM
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Name of town is Required")]
        [Display(Name ="Град")]
        public string TownName { get; set; }
        [Display(Name = "Население")]
        public int Population { get; set; }
        [Display(Name = "Площ")]
        public double Area { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool Validate()
        {
            if (!String.IsNullOrEmpty(TownName) && Population != 0 && Area != 0)
            {
                return true;
            }
            return false;

        }
        public TownVM() { }
        public TownVM(TownDTO townDTO) {
            Id = townDTO.Id;
            TownName = townDTO.TownName;
            Population = townDTO.Population;
            Area = townDTO.Area;
            CreatedOn = townDTO.CreatedOn;
            UpdatedOn = townDTO.UpdatedOn;
            CreatedBy = townDTO.CreatedBy;
            UpdatedBy = townDTO.UpdatedBy;
    }

    }
}
