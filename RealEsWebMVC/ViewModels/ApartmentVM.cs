using Service.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEsWebMVC.ViewModels
{
    public class ApartmentVM
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Apartment type is Required")]
        [Display(Name = "Заглавие")]
        public string ApartmentType { get; set; }
        public int TownId { get; set; }
        public TownDTO Town { get; set; }
        [Display(Name = "Площ")]
        public double Area { get; set; }
        public string Image { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        

    }
}

