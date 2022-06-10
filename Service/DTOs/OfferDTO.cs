using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class OfferDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }
        public int ApartmentId { get; set; }
        //public int TownId? { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }


    }
}
