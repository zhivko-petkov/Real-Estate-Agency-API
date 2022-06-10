using Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
   public class REDbContext : DbContext
    {
        public DbSet<Town> Towns { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}
