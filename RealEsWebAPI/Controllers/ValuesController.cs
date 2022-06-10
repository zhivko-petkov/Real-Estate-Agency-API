using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEsWebAPI.Infrastructure;
using RealEsWebAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private AuthRepository _repo = null;
        public ValuesController()
        {
            _repo = new AuthRepository();
        }
        /*// GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // POST api/Accounts/Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            /*IActionResult errorResult = GetErrorResult(result);*/

            /*if (errorResult != null)
            {
                return errorResult;
            }*/

            return Ok();
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                 _repo.Dispose();
            }

          base.Dispose(disposing);
        }*/

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
