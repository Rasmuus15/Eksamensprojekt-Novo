using System;
using System.Collections.Generic;
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

        public void CreateProjectToDatabase(string projectName, string complexity, DateTime? startDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertProjectQuery = "INSERT INTO PROJECT (ProjectName, Complexity) VALUES (@ProjectName, @Complexity); SELECT SCOPE_IDENTITY();";
                        int projectId = 0;

                        using (SqlCommand cmd = new SqlCommand(insertProjectQuery, connection, transaction))
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
                            using (SqlCommand lengthCmd = new SqlCommand(insertLengthQuery, connection, transaction))
                            {
                                lengthCmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                                lengthCmd.Parameters.AddWithValue("@ProjectId", projectId);
                                lengthCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw; 
                    }
                }
            }
        }

        public List<Models.Project> GetAllProjects()
        {
            return project;
        }
        public void CreateProject(Models.Project newProject)
        {
            project.Add(newProject);
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
