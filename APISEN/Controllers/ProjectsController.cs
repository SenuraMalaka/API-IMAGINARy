using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace ApiSenDS.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {



        // GET api/projects/1
        /// <summary>
        /// Get The Summary of Projects by ManagerID
        /// </summary>
        /// <param name="id">Manager ID</param>
        /// <returns>Json Array</returns>
        /// <response code="200">Json Array returned with Project Summary. An Empty Json Array will be returned if there is no records</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpGet("{id}")]
        public IEnumerable<ProjectsResultSummary> Get(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;


            return context.GetProjectSummary(id);

        }



    }
}
