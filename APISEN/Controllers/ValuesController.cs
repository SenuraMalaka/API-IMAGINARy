using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        //public IEnumerable<string> Get()
        //{
        //    Models.SenDBContext context = HttpContext.RequestServices.GetService(typeof(NetCoreWebApp.Models.MusicStoreContext)) as MusicStoreContext;

        //    return context.GetAllDevelopers();

        //    //return new string[] { "value1", "value2" };
        //}



        // GET api/values
        /// <summary>
        /// Get All the Developers
        /// </summary>
        [HttpGet]
        public IEnumerable<Models.Developers> Get()
        {
            Models.SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

            return context.GetAllDevelopers();  

            //return new string[] { "value1", "value2" };
        }




        // GET api/values/5
        /// <summary>
        /// Get a Specific Developer Details
        /// </summary>
        [HttpGet("{id}",Name ="GetDevByID" )]
        public String Get(int id)
        {
            Models.SenDBContext context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

           // List < Developers > developer1= context.GetADeveloper(id.ToString());

            DevResult devRes = new DevResult()
            {
                Result = context.GetADeveloper(id.ToString()),
            };

            //var json = developer1;
            //json.Add(new Developers { Id = p1 });
            //Developers p1 = new Developers { Id = , Name = 20 };

            String a = JsonConvert.SerializeObject(devRes, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

            return a;

            //return "value";
        }

        // POST api/values

        /// <summary>
        /// Create A Developer
        /// </summary>
        [HttpPost]
        public IActionResult CreateANewDeveloper([FromBody]Developers newDevData)
        {
            if (newDevData == null)
            {
                return BadRequest();
            }else{}

            Models.SenDBContext _context = HttpContext.RequestServices.GetService(typeof(TodoApi.Models.SenDBContext)) as Models.SenDBContext;

            int status = _context.InsertNewDeveloper(newDevData.Name);


            return CreatedAtRoute("GetDevByID", new { id = status }, newDevData);
        }


        //[HttpPost]
        //public IActionResult Create([FromBody] TodoItem item)
        //{
        //    if (item == null)
        //    {
        //        return BadRequest();
        //    }

        //    _context.TodoItems.Add(item);
        //    _context.SaveChanges();

        //    return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        //}



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
