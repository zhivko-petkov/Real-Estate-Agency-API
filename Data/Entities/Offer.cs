using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Offer : BaseEntity
    {
        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }
        //public int TownId { get; set; }
        [Required(ErrorMessage = "ApartmentId is Required")]
        public int ApartmentId { get; set; }
        
        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }

        //[ForeignKey("TownId")]
        //public virtual Town Town { get; set; }


    }
}
