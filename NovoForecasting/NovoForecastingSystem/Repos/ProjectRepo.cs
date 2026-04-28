using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;


namespace NovoForecastingSystem.Repos
{
    public class ProjectRepo : DatabaseConnector
    {
        private List<Models.Project> project;
        public ProjectRepo()
        {
            project = new List<Models.Project>();
        }

        public void CreateProject(string projectName, string complexity, DateTime? startDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                        string insertProjectQuery = "INSERT INTO PROJECT (ProjectName, Complexity) VALUES (@ProjectName, @Complexity); SELECT SCOPE_IDENTITY();";
                        int projectId = 0;

                        using (SqlCommand cmd = new SqlCommand(insertProjectQuery, connection))
                        {
                            cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = projectName;
                            cmd.Parameters.Add("@Complexity", SqlDbType.NVarChar).Value = complexity;

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
                                lengthCmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
                                lengthCmd.Parameters.AddWithValue("@ProjectId", projectId);
                                lengthCmd.ExecuteNonQuery();
                            }
                        }
                        project.Add(new Models.Project 
                        { 
                            ProjectName = projectName, 
                            StartDate = startDate ?? default(DateTime) 
                        });


                       
                    }
                 
                }
            
        

        public List<Models.Project> GetAllProjects()
        {
            return project;
        }

        public void DeleteProject(Models.Project projectToDelete)
        {
            project.Remove(projectToDelete);
        }

        public void EditProject(Models.Project oldProject, Models.Project newProject)
        {
            int index = project.IndexOf(oldProject);
            if (index != -1)
            {
                project[index] = newProject;
            }
        }

    }
}
