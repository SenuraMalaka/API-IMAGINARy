using System;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController:Controller
    {


        //        {
        //    "did":2,
        //    "pid":2,
        //    "date":"06/06/2018",
        //    "hours":4,
        //    "description":"worked 4 hours"
        //}


        // POST api/tasks

        /// <summary>
        /// Create A Daily Record
        /// </summary>
        /// <returns>Http Response</returns>
        /// <response code="200">Updated the record</response>
        /// <response code="500">Database Is Not Online</response>
        /// <response code="400">Invalid json</response>
        /// <response code="201">Record Created Successfully</response>
        [HttpPost]
        public IActionResult CreateANewDailyRecord([FromBody]DeveloperDailyReport reportingData)
        {
            if (reportingData == null)
            {
               return BadRequest();
            }
         

            Models.SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

            int status = _context.InsertNewDeveloperDailyRecord(reportingData);

            if (status == 1)
                return StatusCode(201,"Created the Resource");
            else
                return BadRequest();
        }







        //        {
        //    "did":2,
        //    "pid":2,
        //    "date":"06/07/2018", //MM/dd/yyyy
        //    "hours":5,
        //    "description":"worked 5 hours"
        //}

        // PUT api/tasks/

        /// <summary>
        /// Update A Specific Developer Daily Record
        /// </summary>
        /// <returns>Http Response</returns>
        /// <response code="200">Updated the record</response>
        /// <response code="500">Database Is Not Online</response>
        /// <response code="400">Update resource not available</response>
        [HttpPut]
        public IActionResult updateDevData([FromBody]DeveloperDailyReport updatingData)
        {
            if (updatingData == null)
            {
                return BadRequest();
            }
            Models.SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

            int status = _context.updateDeveloperDailyRecord(updatingData);

            if (status == 1)
                return Ok();
            else
                return BadRequest();
                
        }





        // DELETE api/tasks/1/2/06-26-2018
        /// <summary>
        /// Delete A Specific Developer Daily Record
        /// </summary>
        /// <param name="did">Developer ID</param>
        /// <param name="pid">Project ID</param>
        /// <param name="date">Recorded Date Eg. MM-dd-yyyy</param>
        /// <returns>Http Response Code</returns>
        /// <response code="200">Deleted the record</response>
        /// <response code="500">Database Is Not Online</response>
        /// <response code="400">Delete resource not available</response>
        [HttpDelete("{did}/{pid}/{date}")]
        public IActionResult deleteDevData(int did, int pid, DateTime date)
        {
            if (did == 0 || pid == 0 || date.Equals(DateTime.MinValue))
            {
                return BadRequest();
            }
            Models.SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

            int status = _context.deleteDeveloperDailyRecord(did,pid,date);

            if (status == 1){
                return Ok();
            }  
            else{
                return BadRequest();
            }
                

        }






    }
}
