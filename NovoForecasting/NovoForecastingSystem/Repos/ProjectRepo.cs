using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using NovoForecastingSystem.Models;

namespace NovoForecastingSystem.Repos
{
    public class ProjectRepo : DatabaseConnector
    {
        private List<Project> projects;
        public ProjectRepo()
        {
            projects = new List<Project>();
        }

        public void CreateProject(string projectName, string complexity, DateOnly? startDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                        string insertProjectQuery = "INSERT INTO PROJECT (ProjectName, Complexity) VALUES (@ProjectName, @Complexity); SELECT SCOPE_IDENTITY();";
                        int projectId = 0;

                        using (SqlCommand cmd = new SqlCommand(insertProjectQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@ProjectName", projectName);
                            cmd.Parameters.AddWithValue("@Complexity", (object)complexity ?? DBNull.Value);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                projectId = Convert.ToInt32(result);
                            }
                        }

                        if (startDate.HasValue && projectId > 0)
                        {
                            string insertLengthQuery = "INSERT INTO PROJECT_LENGTH (StartDate, ProjectId) VALUES (@StartDate, @ProjectId);";
                            using (SqlCommand lengthCmd = new SqlCommand(insertLengthQuery, connection))
                            {
                                lengthCmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                                lengthCmd.Parameters.AddWithValue("@ProjectId", projectId);
                                lengthCmd.ExecuteNonQuery();
                            }
                        }
                        projects.Add(new Models.Project 
                        { 
                            ProjectName = projectName, 
                            StartDate = startDate ?? default(DateOnly) 
                        });  
                    }         
                }
            
        

        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Project INNER JOIN Project_Length ON Project.ProjectID = Project_Length.ProjectID", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Project project = new Project()
                        {
                            Id = reader.GetInt32(0),
                            ProjectName = (string)reader["ProjectName"],
                            StartDate = DateOnly.FromDateTime((DateTime)reader["StartDate"]),
                            EndDate = DateOnly.FromDateTime((DateTime)reader["EndDate"])
                        };
                    }
                }
            }
            return projects;
        }

        public void DeleteProject(Project projectToDelete)
        {
            projects.Remove(projectToDelete);
        }

        public void EditProject(Models.Project oldProject, Models.Project newProject)
        {
            int index = projects.IndexOf(oldProject);
            if (index != -1)
            {
                projects[index] = newProject;
            }
        }

    }
}
