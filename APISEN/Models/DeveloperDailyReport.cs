using System;
namespace TodoApi.Models
{
        /// <summary>
        /// Developer Daily Report Request Fields
        /// </summary>
    public class DeveloperDailyReport
    {

        public int DID { get; set; }
        public int PID { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public String Description { get; set; }

    }
}
