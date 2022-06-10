using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private OfferManagementService offerService = new OfferManagementService();
        // GET: api/<OfferController>

        [HttpGet]
        public List<OfferDTO> Get()
        {
            return offerService.Get();
        }


        // GET api/<OfferController>/5
        [HttpGet("{id}")]
        public OfferDTO Get(int id)
        {
            return offerService.GetById(id);
        }

        // POST api/<OfferController>
        [HttpPost]
        public void Post([FromBody] OfferDTO offerDTO)
        {
            offerService.Save(offerDTO);
        }

        // PUT api/<OfferController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OfferDTO offerDTO)
        {
            offerService.Update(offerDTO);
        }

        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            offerService.Delete(id);
        }
    }
}
