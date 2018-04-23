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
        /// Get a Specific Project Details
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns>Json Object</returns>
        /// <response code="200">Json Object with Project Info</response>
        /// <response code="204">No Content To Display. Project is not in the Database.</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpGet("{id}", Name = "GetProjectInfo")]
        public Projects Get(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            return context.GetProjectInfoByID(id.ToString());

        }



        // GET api/projects/track/1
        /// <summary>
        /// Get The Summary of Projects by ManagerID
        /// </summary>
        /// <param name="id">Manager ID</param>
        /// <returns>Json Array</returns>
        /// <response code="200">Json Array returned with Project Summary. An Empty Json Array will be returned if there is no records</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpGet("track/{id}")]
        public IEnumerable<ProjectsResultSummary> GetSummary(int id)
        {
            SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;


            return context.GetProjectSummary(id);

        }





        // POST api/projects
        /// <summary>
        /// Create A Project
        /// </summary>
        /// <param name="name">Project Name</param>
        /// <returns>Json Object with Project Info URI</returns>
        /// <response code="201">Created A New Project</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Database Is Not Online</response>
        [HttpPost]
        public IActionResult CreateANewDeveloper([FromBody]NewProject newProjData)
        {
            if (newProjData == null || newProjData.Name == null)
            {
                return BadRequest();
            }

            SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as SenDBContext;

            String newDeveloperID = _context.InsertNewDeveloper(newProjData.Name);

            if (newDeveloperID == null) return BadRequest();
            else
                return CreatedAtRoute("GetProjectInfo", new { id = newDeveloperID }, newProjData);
        }



    }
}
