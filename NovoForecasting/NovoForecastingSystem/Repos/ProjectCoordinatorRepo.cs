using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using NovoForecastingSystem.Models;

namespace NovoForecastingSystem.Repos
{
    public class ProjectCoordinatorRepo : DatabaseConnector
    {
        private List<ProjectCoordinator> projectCoordinators;
        public ProjectCoordinatorRepo()
        {
            projectCoordinators = new List<ProjectCoordinator>();
        }
        public List<ProjectCoordinator> GetAllProjectCoordinators()
        {
            List<ProjectCoordinator> projectCoordinators = new List<ProjectCoordinator>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM PROJECT_COORDINATOR", connection);
                using(SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ProjectCoordinator projectCoordinator = new ProjectCoordinator()
                        {
                            CoordinatorId = dataReader.GetInt32(0),
                            Initials = (string)dataReader["Initials"].ToString()
                        };
                        projectCoordinators.Add(projectCoordinator);

                    }
                }
            }
            return projectCoordinators;
        }


    }
}
