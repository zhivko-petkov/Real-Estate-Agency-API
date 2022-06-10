using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Town : BaseEntity
    {
        public Town()
        {
            this.Apartments = new HashSet<Apartment>();
        }

        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Name of town is Required")]
        public string TownName { get; set; }
        public int Population { get; set; }
        public double Area { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }


    }
}
