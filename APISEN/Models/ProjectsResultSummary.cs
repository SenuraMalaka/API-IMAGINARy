using System;
namespace TodoApi.Models
{
    public class ProjectsResultSummary
    {
        public string Project { get; set; }
        public string MemberName { get; set; }
        public int TotalHours { get; set; }
        public int TotalOvertime { get; set; }
        public string Contribution { get; set; }


    }
}
