using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace NovoForecastingSystem.Repos
{
    public class ResourceRepo : DatabaseConnector
    {
        public List<Resource> resources;
        public List<Resource> ProcessEngineers;
        public List<Resource> ChemicalEngineers;
        public List<Resource> SoftwareEngineer;

        public ResourceRepo()
        {
            resources = new List<Resource>();
        }



        public List<string> PrintEmail(string jobRole)
        {
            List<string> EmailList = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("FindEmails", connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@JobRole", System.Data.SqlDbType.NVarChar).Value = jobRole;

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


        public void UpdateResourceProject(string email, int projectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RESOURCE SET ProjectId = @ProjectId, Availability = 0 WHERE Email = @Email", connection);
                cmd.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int).Value = projectId;
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = email;
                cmd.ExecuteNonQuery();
            }
        }

        public List<Resource> GetResourcesByRole(JobRole role)
        {
            List<Resource> resourcesByRole = new List<Resource>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM resource WHERE JobRole = @JobRole AND Availability = 0", connection);
                cmd.Parameters.AddWithValue("@JobRole", role.ToString());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Enum.TryParse<JobRole>((string)reader["JobRole"], out JobRole jobRole);
                        Resource resource = new Resource()
                        {
                            Id = reader.GetInt32(0),
                            Initials = (string)reader["Initials"],
                            Email = (string)reader["Email"],
                            Availability = (bool)reader["Availability"],
                            JobRoleEnum = jobRole
                        };
                        resourcesByRole.Add(resource);
                    }
                }
            }
            return resourcesByRole;
        }
    }
}
