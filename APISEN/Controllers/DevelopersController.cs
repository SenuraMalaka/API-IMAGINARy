using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace ApiSenDS.Controllers
{
    
    [Route("api/[controller]")]
    public class DevelopersController: Controller
    {
        // GET api/Developers/ot/1
        /// <summary>
        /// Get The OverTime for a  Developer
        /// </summary>
        /// <param name="id">Developer ID</param>
        /// <returns>Json Object</returns>
        /// <response code="200">Json Object with OverTime Value</response>
        /// <response code="204">No Content To Display</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpGet("{ot}/{id}")]
        public OTResult Get(int id,String ot)
        {
            if (ot != "ot")
            {
                return null;//status 204
            }
            else
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
                else{
                    return null;//no overtime val, Thus, No response.//status 204
                }


            }
        }



        // POST api/developers
        /// <summary>
        /// Create A Developer
        /// </summary>
        [HttpPost]
        public IActionResult CreateANewDeveloper([FromBody]Developers newDevData)
        {
            if (newDevData == null)
            {
                return BadRequest();
            }

            SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            int status = _context.InsertNewDeveloper(newDevData.Name);


            return CreatedAtRoute("GetDevByID", new { id = status }, newDevData);
        }



    }
}
