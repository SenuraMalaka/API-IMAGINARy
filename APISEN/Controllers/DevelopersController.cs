using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace ApiSenDS.Controllers
{

    [Route("api/[controller]")]
    public class DevelopersController : Controller
    {


        // GET api/developers/1
        /// <summary>
        /// Get a Specific Developer Details
        /// </summary>
        /// <param name="id">Developer ID</param>
        /// <returns>Json Object</returns>
        /// <response code="200">Json Object with Developer Info</response>
        /// <response code="204">No Content To Display. Developer is not in the Database.</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpGet("{id}", Name = "GetDeveloperInfo")]
        public Developers Get(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            return context.GetADeveloperNameByID(id.ToString());

        }





        // GET api/Developers/ot/1
        /// <summary>
        /// Get The OverTime for a  Developer
        /// </summary>
        /// <param name="id">Developer ID</param>
        /// <returns>Json Object</returns>
        /// <response code="200">Json Object with OverTime Value</response>
        /// <response code="204">No Content To Display</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpGet("ot/{id}")]
        public OTResult GetOverTime(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            String overTimeVal = context.GetDeveloperOverTime(id);

            if (overTimeVal != "")
            {

                OTResult result = new OTResult()
                {
                    OverTime = overTimeVal,
                    DeveloperID = id
                };

                return result;
            }
            else
            {
                return null;//no overtime val, Thus, No response.//status 204
            }


        }



        // POST api/developers
        /// <summary>
        /// Create A Developer
        /// </summary>
        /// <param name="name">Developer Name</param>
        /// <returns>Json Object with Developer Info URI</returns>
        /// <response code="201">Created A New Developer</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpPost]
        public IActionResult CreateANewDeveloper([FromBody]NewDeveloper newDevData)
        {
            if (newDevData==null ||newDevData.Name == null)
            {
                return BadRequest();
            }

            SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            String newDeveloperID = _context.InsertNewDeveloper(newDevData.Name);

            if (newDeveloperID==null) return BadRequest();
            else
                return CreatedAtRoute("GetDeveloperInfo", new { id = newDeveloperID }, newDevData);
        }



    }
}
