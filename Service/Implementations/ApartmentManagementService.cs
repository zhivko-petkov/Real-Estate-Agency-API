using Data.Entities;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ApartmentManagementService
    {
        TownManagementService townService = new TownManagementService();

        public List<ApartmentDTO> Get()
        {

            List<ApartmentDTO> apartmentsDTO = new List<ApartmentDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.ApartmentRepository.Get())
                {

                    ApartmentDTO apartmentDTO = new ApartmentDTO();
                    apartmentDTO.Id = item.Id;
                    apartmentDTO.ApartmentType = item.ApartmentType;
                    apartmentDTO.Area = item.Area;
                    apartmentDTO.Image = item.Image;
                    apartmentDTO.Description = item.Description;
                    apartmentDTO.CreatedBy = item.CreatedBy;
                    apartmentDTO.TownId = item.TownId;
                    apartmentDTO.UpdatedBy = item.UpdatedBy;
                    apartmentDTO.Town = townService.GetById(item.TownId);

                    if (item.CreatedOn != null)
                        apartmentDTO.CreatedOn = (DateTime)item.CreatedOn;

                    if (item.UpdatedOn != null)
                        apartmentDTO.UpdatedOn = (DateTime)item.UpdatedOn;

                    apartmentsDTO.Add(apartmentDTO);

                }
            }
            return apartmentsDTO;
        }
        public ApartmentDTO GetById(int id)
        {
            ApartmentDTO apartmentDTO = new ApartmentDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Apartment apartment = unitOfWork.ApartmentRepository.GetByID(id);
                if (apartment != null)
                {
                    apartmentDTO = new ApartmentDTO
                    {
                        Id = apartment.Id,
                        Area = apartment.Area,
                        ApartmentType = apartment.ApartmentType,
                        Image = apartment.Image,
                        TownId = apartment.TownId,
                        Description = apartment.Description,
                        Town = townService.GetById(apartment.TownId),
                        CreatedBy = apartment.CreatedBy,
                        UpdatedBy = apartment.UpdatedBy

                    };
                    if (apartment.CreatedOn != null)
                        apartmentDTO.CreatedOn = (DateTime)apartment.CreatedOn;

                    if (apartment.UpdatedOn != null)
                        apartmentDTO.UpdatedOn = (DateTime)apartment.UpdatedOn;
                }
            }

            return apartmentDTO;
        }

        public bool Save(ApartmentDTO apartmentDTO)
        {
            //  UnitOfWork unitOfWorkk = new UnitOfWork();
            Apartment apartment = new Apartment
            {
               // Id = apartmentDTO.Id,
                ApartmentType = apartmentDTO.ApartmentType,
                Description = apartmentDTO.Description,
                Area = apartmentDTO.Area,
                Image = apartmentDTO.Image,
                TownId = apartmentDTO.TownId,
                //  Town = unitOfWorkk.TownRepository.GetByID(apartmentDTO.TownId),
                CreatedOn = DateTime.UtcNow,
                CreatedBy = apartmentDTO.CreatedBy


                //CreatedOn = townDTO.CreatedOn
            };

            /*  if (apartmentDTO.CreatedOn != null)
                  apartment.CreatedOn = (DateTime)apartmentDTO.CreatedOn;*/

            if (apartmentDTO.UpdatedOn != null)
                apartment.UpdatedOn = (DateTime)apartmentDTO.UpdatedOn;

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (apartmentDTO.Id == 0)
                    {
                        unitOfWork.ApartmentRepository.Insert(apartment);
                    }
                    else
                    {
                        apartment.Id = apartmentDTO.Id;
                        unitOfWork.ApartmentRepository.Update(apartment);
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
                    Apartment apartment = unitOfWork.ApartmentRepository.GetByID(id);
                    unitOfWork.ApartmentRepository.Delete(apartment);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(ApartmentDTO apartmentDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Apartment currentApartment = unitOfWork.ApartmentRepository.GetByID(apartmentDTO.Id);
                    currentApartment.ApartmentType = apartmentDTO.ApartmentType;
                    currentApartment.Area = apartmentDTO.Area;
                    currentApartment.Image = apartmentDTO.Image;
                    currentApartment.TownId = apartmentDTO.TownId; 
                    currentApartment.Town = unitOfWork.TownRepository.GetByID(apartmentDTO.Town.Id);
                    currentApartment.Description = apartmentDTO.Description;
                    currentApartment.CreatedOn = DateTime.UtcNow;

                    if (currentApartment.CreatedOn != null)
                        currentApartment.CreatedOn = apartmentDTO.CreatedOn;


                    currentApartment.CreatedBy = apartmentDTO.CreatedBy;

                    currentApartment.UpdatedOn = DateTime.Now;
                    //currentApartment.UpdatedBy = apartmentDTO.UpdatedBy;
                    unitOfWork.ApartmentRepository.Update(currentApartment);
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
