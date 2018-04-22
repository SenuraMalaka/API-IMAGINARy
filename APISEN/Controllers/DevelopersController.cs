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
        [HttpGet("{id}")]
        public OTResult Get(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            OTResult result = new OTResult() { OverTime= context.GetDeveloperOverTime(id),
                DeveloperID =id
            };

            return result;//"{\"overtime\":\""+context.GetDeveloperOverTime(id)+"\"}";
        }
    }
}
