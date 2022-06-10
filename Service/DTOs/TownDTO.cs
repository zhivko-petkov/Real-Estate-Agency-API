using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class TownDTO
    {
        public int Id { get; set; }
        public string TownName { get; set; }
        public int Population { get; set; }
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

    }
}
