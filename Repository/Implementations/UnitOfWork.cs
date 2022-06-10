using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class UnitOfWork : IDisposable
    {
       
        private REDbContext context = new REDbContext();
        private GenericRepository<Apartment> apartmentRepository;
        private GenericRepository<Offer> offerRepository;
        private GenericRepository<Town> townRepository;

        public GenericRepository<Apartment> ApartmentRepository
        {
            get
            {

                if (this.apartmentRepository == null)
                {
                    this.apartmentRepository = new GenericRepository<Apartment>(context);
                }
                return apartmentRepository;
            }
        }

        public GenericRepository<Offer> OfferRepository
        {
            get
            {
                if (this.offerRepository == null)
                {
                    this.offerRepository = new GenericRepository<Offer>(context);
                }
                return offerRepository;
            }
        }

        public GenericRepository<Town> TownRepository
        {
            get
            {
                if (this.townRepository == null)
                {
                    this.townRepository = new GenericRepository<Town>(context);
                }
                return townRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
