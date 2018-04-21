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


            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into dev_project (did,pid,date,hours,description) " +
                                                    "values ("+devReportData.DID+","+devReportData.PID+"," 
                                                    +"\'"+devReportData.Date.ToString("yyyy-MM-dd")+"\'"+","+devReportData.Hours+","+
                                                    "\'" + devReportData.Description + "\')", conn);

                status = cmd.ExecuteNonQuery();

            }
            return status;
        }


        public int updateDeveloperDailyRecord(DeveloperDailyReport devReportData)
        {

            int status = 0;


            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                String upQuery = "UPDATE dev_project SET date = " + "\'" + devReportData.Date.ToString("yyyy-MM-dd") + "\'" + ", hours = " + devReportData.Hours +
                                                                                      " WHERE did=" + devReportData.DID + " AND " + "pid=" + devReportData.PID +
                                                                                        " AND date=" + "\'" + devReportData.Date.ToString("yyyy-MM-dd")+ "\'";

                MySqlCommand cmd = new MySqlCommand(upQuery, conn);


                status = cmd.ExecuteNonQuery();

            }
            return status;
        }


    }
}
