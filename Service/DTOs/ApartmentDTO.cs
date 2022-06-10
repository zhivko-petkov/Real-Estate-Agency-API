using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class ApartmentDTO
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Apartment type is Required")]
        public string ApartmentType { get; set; }
        public double Area { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int TownId { get; set; }
        public TownDTO Town { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool Validate()
        {
            if (!String.IsNullOrEmpty(ApartmentType) && !String.IsNullOrEmpty(Image) && Area != 0)
            {
                return true;
            }
            return false;

        }
    }
}
