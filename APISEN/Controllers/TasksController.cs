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
        [HttpPost]
        public IActionResult CreateANewDeveloper([FromBody]DeveloperDailyReport reportingData)
        {
            if (reportingData == null)
            {
               return BadRequest();
            }
         

            Models.SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

            int status = _context.InsertNewDeveloperDailyRecord(reportingData);

            if (status == 1)
                return Ok();
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

        // PUT api/tasks/2

        /// <summary>
        /// Update A Specific Developer Daily Record
        /// </summary>
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



        //        {
        //    "did":2,
        //    "pid":2,
        //    "date":"06/07/2018",
        //}

        // DELETE api/tasks
        /// <summary>
        /// Delete A Specific Developer Daily Record
        /// </summary>
        [HttpDelete]
        public IActionResult deleteDevData([FromBody]DeveloperDailyReport updatingData)
        {
            if (updatingData == null)
            {
                return BadRequest();
            }
            Models.SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

            int status = _context.deleteDeveloperDailyRecord(updatingData);

            if (status == 1)
                return Ok();
            else
                return BadRequest();

        }






    }
}
