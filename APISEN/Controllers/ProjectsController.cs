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
        [HttpGet("{id}")]
        public IEnumerable<ProjectsResultSummary> Get(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            return context.GetProjectSummary(id);

        }



    }
}
