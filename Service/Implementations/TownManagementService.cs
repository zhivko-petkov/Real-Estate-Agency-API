using Data.Entities;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class TownManagementService
    {
        public List<TownDTO> Get()
        {
            List<TownDTO> townsDTO = new List<TownDTO>();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.TownRepository.Get())
                {

                    TownDTO townDTO = new TownDTO();
                    townDTO.Id = item.Id;
                    townDTO.TownName = item.TownName;
                    townDTO.Area = item.Area;
                    townDTO.Population = item.Population;
                    townDTO.CreatedBy = item.CreatedBy;
                    townDTO.UpdatedBy = item.UpdatedBy;

                    if (item.CreatedOn != null)
                        townDTO.CreatedOn = (DateTime)item.CreatedOn;

                    if (item.UpdatedOn != null)
                        townDTO.UpdatedOn = (DateTime)item.UpdatedOn;

                    townsDTO.Add(townDTO);

                }
            }
            return townsDTO;
        }
        public TownDTO GetById(int id)
        {
            TownDTO townDTO = null;

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Town town = unitOfWork.TownRepository.GetByID(id);
                if (town != null)
                {
                    townDTO = new TownDTO
                    {
                        Id = town.Id,
                        TownName = town.TownName,
                        Area = town.Area,
                        Population = town.Population,
                        CreatedBy = town.CreatedBy,
                        CreatedOn = (DateTime)town.CreatedOn,
                        UpdatedBy = town.UpdatedBy,

                    };
                    if (town.UpdatedOn != null)
                        townDTO.UpdatedOn = (DateTime)town.UpdatedOn;
                }
            }

            return townDTO;
        }

        public bool Save(TownDTO townDTO)
        {

            Town town = new Town
            {
                Id = townDTO.Id,
                TownName = townDTO.TownName,
                Population = townDTO.Population,
                Area = townDTO.Area,
                CreatedBy = townDTO.CreatedBy,
                CreatedOn = townDTO.CreatedOn

                //CreatedBy = townDTO.CreatedBy, 
                //CreatedOn = townDTO.CreatedOn
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (townDTO.Id == 0)
                        unitOfWork.TownRepository.Insert(town);
                    else
                        unitOfWork.TownRepository.Update(town);

                    unitOfWork.Save();
                }

                //Console.WriteLine(student);
                //ctx.Students.Add(student);
                //ctx.SaveChanges();

                return true;
            }
            catch
            {
                Console.WriteLine(town);
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
                    Town town = unitOfWork.TownRepository.GetByID(id);
                    unitOfWork.TownRepository.Delete(town);
                    unitOfWork.Save();
                }
                //Student student = ctx.Students.Find(id);
                //ctx.Students.Remove(student);
                //ctx.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(TownDTO townDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Town currentTown = unitOfWork.TownRepository.GetByID(townDTO.Id);
                    currentTown.TownName = townDTO.TownName;
                    currentTown.Population = townDTO.Population;
                    currentTown.Area = townDTO.Area;
                    currentTown.CreatedBy = townDTO.CreatedBy;
                    if (currentTown.CreatedOn != null)
                        currentTown.CreatedOn = townDTO.CreatedOn;

                    
                        currentTown.UpdatedBy = townDTO.CreatedBy;

                    currentTown.UpdatedOn = DateTime.Now;
                    unitOfWork.TownRepository.Update(currentTown);
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
