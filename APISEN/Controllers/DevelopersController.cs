using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace ApiSenDS.Controllers
{
    
    [Route("api/[controller]")]
    public class DevelopersController: Controller
    {
        // GET api/projects/1
        /// <summary>
        /// Get The OverTime for a  Developer
        /// </summary>
        /// <param name="id">Developer ID</param>
        /// <returns>Json Object</returns>
        /// <response code="200">Json Object with OverTime Value</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpGet("{id}")]
        public OTResult Get(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            OTResult result = new OTResult() { OverTime= context.GetDeveloperOverTime(id),
                DeveloperID =id
            };

            return result;
        }
    }
}
