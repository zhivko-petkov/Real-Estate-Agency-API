using Data.Entities;
using Repository.Implementations;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class OfferManagementService
    {
        //ApartmentManagementService apartmentManagementService = new ApartmentManagementService();
        public List<OfferDTO> Get()
        {
            List<OfferDTO> offersDTOlist = new List<OfferDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.OfferRepository.Get())
                {
                    OfferDTO offersDTO = new OfferDTO();
                    offersDTO.Id = item.Id;
                    offersDTO.Price = item.Price;
                    offersDTO.CreatedBy = item.CreatedBy;
                    offersDTO.UpdatedBy = item.UpdatedBy;
                    //offersDTO.TownId = item.TownId;
                    offersDTO.ApartmentId = item.ApartmentId;

                    if (item.CreatedOn != null)
                        offersDTO.CreatedOn = (DateTime)item.CreatedOn;

                    if (item.UpdatedOn != null)
                        offersDTO.UpdatedOn = (DateTime)item.UpdatedOn;

                    offersDTOlist.Add(offersDTO);
                }
            }

            return offersDTOlist;
        }

        public OfferDTO GetById(int id)
        {
            OfferDTO offerDTO = null;

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Offer offer = unitOfWork.OfferRepository.GetByID(id);
                if (offer != null)
                {
                    offerDTO = new OfferDTO
                    {
                        Id = offer.Id,
                        Price = offer.Price,
                        ApartmentId = offer.ApartmentId,
                        //TownId = offer.TownId,
                        CreatedBy = offer.CreatedBy,
                        UpdatedBy = offer.UpdatedBy
                    };
                    if (offer.CreatedOn != null)
                        offerDTO.CreatedOn = (DateTime)offer.CreatedOn;

                    if (offer.UpdatedOn != null)
                        offerDTO.UpdatedOn = (DateTime)offer.UpdatedOn;

                }
            }

            return offerDTO;
        }
        public bool Save(OfferDTO offerDTO)
        {

            Offer offer = new Offer
            {
                Price = offerDTO.Price,
                //TownId = offerDTO.TownId,
                ApartmentId = offerDTO.ApartmentId,
                CreatedBy = offerDTO.CreatedBy,
                CreatedOn = DateTime.Now
            };

            try
            { //edited soon
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    //offer.Apartment = unitOfWork.ApartmentRepository.GetByID(offerDTO.ApartmentId);
                    //offer.Town = unitOfWork.TownRepository.GetByID(offerDTO.TownId);
                    if (offerDTO.Id == 0)
                    {   
                        unitOfWork.OfferRepository.Insert(offer);
                    }
                    else
                    {
                        offer.Id = offerDTO.Id;
                        unitOfWork.OfferRepository.Update(offer);
                    }
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            // here the DTO is just an int
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Offer offer = unitOfWork.OfferRepository.GetByID(id);
                    unitOfWork.OfferRepository.Delete(offer);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(OfferDTO offerDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Offer currentOffer = unitOfWork.OfferRepository.GetByID(offerDTO.Id);
                    currentOffer.Id = offerDTO.Id;
                    currentOffer.Price = offerDTO.Price;
                    currentOffer.ApartmentId = offerDTO.ApartmentId;
                    currentOffer.CreatedBy = offerDTO.CreatedBy;
                    currentOffer.Apartment = unitOfWork.ApartmentRepository.GetByID(offerDTO.ApartmentId);
                    //currentOffer.Town = unitOfWork.TownRepository.GetByID(offerDTO.TownId);
                    currentOffer.UpdatedBy = offerDTO.UpdatedBy;
                    currentOffer.CreatedOn = offerDTO.CreatedOn;
                    //currentOffer.TownId = offerDTO.TownId;
                    currentOffer.UpdatedOn = DateTime.Now;
                    unitOfWork.OfferRepository.Update(currentOffer);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
