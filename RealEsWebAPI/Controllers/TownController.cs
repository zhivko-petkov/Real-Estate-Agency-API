using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEsWebAPI.Messages;
using Repository.Implementations;
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
    public class TownController : ControllerBase
    {

        private TownManagementService townService = new TownManagementService();

        /*[BasicAuthorize]*/
        /*[Authorize]*/
        [HttpGet]
        /*[Route("authenticate")]*/
        public List<TownDTO> GetTowns()
        {
            return townService.Get();

        }

        [HttpGet("{id}")]
        public TownDTO Get(int id)
        {
            return townService.GetById(id);
        }

        /*[HttpPost]
         public IHttpActionResult Get(int id)
         {
             return Json(townService.GetById(id));
         }*/

        [HttpPost]
        public string SaveTown([FromBody] TownDTO townDTO)
        {

            ResponseMessage response = new ResponseMessage();
            townDTO.CreatedOn = DateTime.Now;
            if (townService.Save(townDTO))
            {
                response.Code = 200;
                response.Body = "Town is saved.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Town is not saved.";
            }

            return response.Code.ToString() + " " + response.Body.ToString();
        }

        [HttpDelete]
        public string Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (townService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Town is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Town is not deleted.";
            }

            return response.Code.ToString() + " " + response.Body.ToString();
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] TownDTO townDTO)
        {
            ResponseMessage response = new ResponseMessage();
            if (!townDTO.Validate())
                return "500 Data is not valid!";


            if (townService.Update(townDTO))
            {
                response.Code = 200;
                response.Body = "Town is updated.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Town is not updated.";
            }

            return response.Code.ToString() + " " + response.Body.ToString();

        }
    }
}
