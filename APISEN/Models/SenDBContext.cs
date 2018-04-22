﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace TodoApi.Models
{
    public class SenDBContext
    {
        public SenDBContext()
        {
        }



        public string ConnectionString { get; set; }  
  
        public SenDBContext(string connectionString)  
        {  
            this.ConnectionString = connectionString;  
        }  
  
        private MySqlConnection GetConnection()  
        {  
            return new MySqlConnection(ConnectionString);  
        }




        public List<Developers> GetAllDevelopers()  
        {  
            List<Developers> list = new List<Developers>();  
  
            using (MySqlConnection conn = GetConnection())  
            {  
                conn.Open();  
                MySqlCommand cmd = new MySqlCommand("select * from developers", conn);  
  
                using (var reader = cmd.ExecuteReader())  
                {  
                    while (reader.Read())  
                    {  
                        list.Add(new Developers()  
                        {  
                            Id = Convert.ToInt32(reader["did"]),  
                            Name = reader["name"].ToString(),   
                        });  
                    }  
                }  
                }  
                return list;  
          }






        public List<ProjectsResultSummary> GetProjectSummary(int managerID)  
        {  
            List<ProjectsResultSummary> list = new List<ProjectsResultSummary>();  
  
            using (MySqlConnection conn = GetConnection())  
            {

                String _ProjectsSummaryQuery = "select p.name as 'Project', d.name as 'Members', sum(dpr.hours) as 'Total Hours', sum(dpr.ot) as 'Total OverTime'," +
                    " concat(round(( sum(dpr.hours)/(select sum(dpr.hours) from project p, developers d,devProject dp, devProjectReports dpr, managers m where p.mid="+managerID+" " +
                    "and p.mid=m.mid and dp.dpid=dpr.dpid and d.did=dp.did and p.pid=dp.pid) * 100 ),2),'%') AS 'Contribution' from project p, developers d,devProject dp, " +
                    "devProjectReports dpr, managers m where p.mid="+managerID+" and p.mid=m.mid and dp.dpid=dpr.dpid and d.did=dp.did and p.pid=dp.pid "+
                    "group by dpr.dpid";


                conn.Open();  
                MySqlCommand cmd = new MySqlCommand(_ProjectsSummaryQuery, conn);  
  
                using (var reader = cmd.ExecuteReader())  
                {  
                    while (reader.Read())  
                    {  
                        list.Add(new ProjectsResultSummary()  
                        {  
                            Project = reader["Project"].ToString(),
                            MemberName=reader["Members"].ToString(),
                            TotalHours=Convert.ToInt32(reader["Total Hours"]),
                            TotalOvertime =Convert.ToInt32(reader["Total Overtime"]),
                            Contribution=reader["Contribution"].ToString()
                        });  
                    }  
                }  
                }  
                return list;  
          } 






        public List<Developers> GetADeveloper(String devID)
        {
            List<Developers> list = new List<Developers>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from developers where did="+devID, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Developers()
                        {
                            Id = Convert.ToInt32(reader["did"]),
                            Name = reader["name"].ToString(),
                        });
                    }
                }
            }
            return list;
        }




        public int InsertNewDeveloper (String devName)
        {
            List<Developers> list = new List<Developers>();

            int status = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into developers (name) values (\'"+devName+"\')", conn);

                status = cmd.ExecuteNonQuery();// .ExecuteReader();

            }
            return status;
        }





        public int InsertNewDeveloperDailyRecord(DeveloperDailyReport devReportData)
        {

            int status = 0;
            int overTimeHours = 0;

            if(devReportData.Hours>8){
                overTimeHours = devReportData.Hours - 8;
            }


            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                String _insertQuery = "insert into devProjectReports (dpid, date, hours, ot, description) " +
                    "values ((select dpid from devProject where did=" + devReportData.DID + " and pid=" + devReportData.PID + ")," +
                                                                                     "'" + devReportData.Date.ToString("yyyy-MM-dd")+ 
                                                                                     "'," + devReportData.Hours + "," + overTimeHours + ",'" + devReportData.Description + "')";


                MySqlCommand cmd = new MySqlCommand(_insertQuery, conn);


                //MySqlCommand cmd = new MySqlCommand("insert into dev_project (did,pid,date,hours,ot,description) " +
                                                    //"values ("+devReportData.DID+","+devReportData.PID+"," 
                                                    //+"\'"+devReportData.Date.ToString("yyyy-MM-dd")+"\'"+","+devReportData.Hours+","+overTimeHours+","+
                                                    //"\'" + devReportData.Description + "\')", conn);

                status = cmd.ExecuteNonQuery();

            }
            return status;
        }


        public int updateDeveloperDailyRecord(DeveloperDailyReport devReportData)
        {

            int status = 0;
            int overTimeHours = 0;

            if (devReportData.Hours > 8)
            {
                overTimeHours = devReportData.Hours - 8;
            }


            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                String upQuery = "UPDATE devProjectReports SET date = " + "\'" + devReportData.Date.ToString("yyyy-MM-dd") + "\'" + ", hours = " + devReportData.Hours +
                                                                                        " , ot="+overTimeHours+", description="+"\'"+devReportData.Description+"\'"+
                                                                                              " WHERE dpid=(select dpid from devProject where did=" + devReportData.DID + " AND " + "pid=" + devReportData.PID +")"+
                                                                                        " AND date=" + "\'" + devReportData.Date.ToString("yyyy-MM-dd")+ "\'";

                MySqlCommand cmd = new MySqlCommand(upQuery, conn);


                status = cmd.ExecuteNonQuery();

            }
            return status;
        }



        public int deleteDeveloperDailyRecord(DeveloperDailyReport devReportData)
        {

            int status = 0;


            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                String delQuery = "DELETE FROM devProjectReports WHERE dpid=(select dpid from devProject where did=" + devReportData.DID + " AND " + "pid=" + devReportData.PID + ")" +
                                                                                    " AND date=" + "\'"
                                                                                    + devReportData.Date.ToString("yyyy-MM-dd") + "\'";


                MySqlCommand cmd = new MySqlCommand(delQuery, conn);


                status = cmd.ExecuteNonQuery();

            }
            return status;
        }

        //SELECT* FROM dev_project WHERE date BETWEEN >= '2018-01-10' AND '2018-05-01';


    }
}
