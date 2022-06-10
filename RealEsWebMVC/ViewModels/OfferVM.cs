using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEsWebMVC.ViewModels
{
    public class OfferVM
    {
        public int Id { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        public int ApartmentId { get; set; }
        //public int TownId { get; set; }
       [Display(Name = "Апартамент" )]
        public string ApartmentName { get; set; }
        
        [Display(Name = "Град")]
        public string TownName { get; set; }
        public string Img { get; set; }
        [Display(Name = "Площ")]
        public double Area { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }
}
