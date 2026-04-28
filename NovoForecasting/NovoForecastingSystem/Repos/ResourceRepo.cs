using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace NovoForecastingSystem.Repos
{
    public class ResourceRepo : DatabaseConnector
    {
        private List<Models.Ressource> ressource;

        public ResourceRepo()
        {
            ressource = new List<Models.Ressource>(); 
        }



        public List<string> PrintEmail(string jobRole)
        {
            List<string> EmailList = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("FindEmails", connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("JobRole", jobRole);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        {
                            EmailList.Add(reader["Email"].ToString()); //Tilføjer en ny kolonne (Email) til EmailListe
                        }
                        
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while retrieving waste data: " + ex.Message);
                }
            }
                return EmailList;
         }

        





        //public void AddResource(Object Phase, Object JobRole)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string insertProjectQuery = "";
        //        int projectId = 0;

        //        using (SqlCommand cmd = new SqlCommand(insertProjectQuery, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@ProjectName", projectName);
        //            cmd.Parameters.AddWithValue("@Complexity", (object)complexity ?? DBNull.Value);

        //            object result = cmd.ExecuteScalar();
        //            if (result != null)
        //            {
        //                projectId = Convert.ToInt32(result);
        //            }
        //        }

        //        if (startDate.HasValue && projectId > 0)
        //        {
        //            string insertLengthQuery = "INSERT INTO PROJECT_LENGTH (StartDate, ProjectId) VALUES (@StartDate, @ProjectId);";
        //            using (SqlCommand lengthCmd = new SqlCommand(insertLengthQuery, connection))
        //            {
        //                lengthCmd.Parameters.AddWithValue("@StartDate", startDate.Value);
        //                lengthCmd.Parameters.AddWithValue("@ProjectId", projectId);
        //                lengthCmd.ExecuteNonQuery();
        //            }
        //        }
        //        project.Add(new Models.Project
        //        {
        //            ProjectName = projectName,
        //            StartDate = startDate ?? default(DateTime)
        //        });



        //    }

        //}


        public List<Models.Ressource> GetAllRessources()
        {
            return ressource;
        }
    }
}
