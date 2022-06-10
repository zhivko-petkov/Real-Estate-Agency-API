using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Apartment : BaseEntity
    {
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Apartment type is Required")]
        public string ApartmentType { get; set; }
        public double Area { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int TownId { get; set; }
        
        [ForeignKey("TownId")]
        public virtual Town Town { get; set; }

    }
}
