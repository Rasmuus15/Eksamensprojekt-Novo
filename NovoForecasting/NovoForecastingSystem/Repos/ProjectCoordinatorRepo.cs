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


        //public ProjectCoordinator? GetPCById( int coordinatorId)
        //{
        //    ProjectCoordinator? PC = null;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open ();
        //        SqlCommand cmd = new SqlCommand("SELECT CoordinatorId, Initials FROM PROJECT_COORDINATOR WHERE CoordinatorId = @CoordinatorId", connection);
        //        cmd.Parameters.AddWithValue("@CoordinatorId", coordinatorId);

        //        using (SqlDataReader dataReader = cmd.ExecuteReader())
        //        {
        //            if (dataReader.Read())
        //            {
        //                ProjectCoordinator pc = new ProjectCoordinator
        //                {
        //                    CoordinatorId = dataReader.GetInt32(0),
        //                    Initials = dataReader.GetString("Initials")
        //                };
        //            }
        //        }

        //    }
        //    return PC;
        //}

        //Create, Delete og Update metoderne, som kan implementeres hvis ønsket

        //public void CreatePC(ProjectCoordinator pcToBeCreated)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

              
        //        using (SqlCommand cmd = new SqlCommand("INSERT INTO PROJECT_COORDINATOR(Initials)" +
        //                                               "VALUES(@Initials);" +
        //                                               "SELECT SCOPE_IDENTITY();", connection))
        //        {
                    
        //            cmd.Parameters.Add("@Initials", SqlDbType.NVarChar,4).Value = pcToBeCreated;

                    
        //            pcToBeCreated.CoordinatorId =Convert.ToInt32(cmd.ExecuteScalar());
        //            projectCoordinators.Add(pcToBeCreated);
        //        }
        //    }
        //}

        //public void DeletePC(int? coordinatorId)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open ();
        //        SqlCommand cmd = new SqlCommand("DELETE FROM PROJECT_COORDINATOR WHERE CoordinatorId = @CoordinatorId", connection);
        //        cmd.Parameters.AddWithValue("@CoordinatorId", coordinatorId);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //public void UpdatePC(ProjectCoordinator pcToBeUpdated)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open ();
        //        SqlCommand cmd = new SqlCommand("UPDATE PROJECT_COORDINATOR SET Initials = @Initials WHERE CoordinatorId = @CoordinatorId", connection);
        //        cmd.Parameters.Add("@Initials", SqlDbType.NVarChar).Value = pcToBeUpdated;
        //        cmd.ExecuteNonQuery();
        //    }
        //}

    }
}
