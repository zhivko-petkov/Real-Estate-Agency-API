using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Implementations;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {

        private ApartmentManagementService apartmentService = new ApartmentManagementService();
        // GET: api/<ApartmentController>
        [HttpGet]
        public IEnumerable<ApartmentDTO> Get()
        {
            return apartmentService.Get();
        }

        // GET api/<ApartmentController>/5
        [HttpGet("{id}")]
        public ApartmentDTO Get(int id)
        {
            return apartmentService.GetById(id);
        }

        // POST api/<ApartmentController>
        [HttpPost]
        public void Post([FromBody] ApartmentDTO apartmentDTO)
        {
            apartmentService.Save(apartmentDTO);


        }

        // PUT api/<ApartmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ApartmentDTO apartmentDTO)
        {
            apartmentService.Update(apartmentDTO);
        }

        // DELETE api/<ApartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            apartmentService.Delete(id);
        }
    }
}
