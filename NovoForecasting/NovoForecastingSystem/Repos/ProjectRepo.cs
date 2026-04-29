using Microsoft.Data.SqlClient;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace NovoForecastingSystem.Repos
{
    public class ProjectRepo : DatabaseConnector
    {
        private List<Project> projects;
        public ProjectRepo()
        {
            projects = new List<Project>();
        }

        public Project CreateProject(string projectName, string complexity, DateOnly startDate, DateOnly endDate, int projectCoordinatorid)
        {
            int projectId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertProjectQuery = "INSERT INTO PROJECT (ProjectName, Complexity, CoordinatorId) VALUES (@ProjectName, @Complexity, @CoordinatorId); SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(insertProjectQuery, connection))
                {
                    cmd.Parameters.Add("@ProjectName", System.Data.SqlDbType.NVarChar, 255).Value = projectName;
                    cmd.Parameters.Add("@Complexity", System.Data.SqlDbType.NVarChar, 50).Value = complexity;
                    cmd.Parameters.Add("@CoordinatorId", System.Data.SqlDbType.Int).Value = projectCoordinatorid;
                    projectId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string insertLengthQuery = "INSERT INTO PROJECT_LENGTH (StartDate, ProjectId, EndDate) VALUES (@StartDate, @ProjectId, @EndDate);";
                using (SqlCommand lengthCmd = new SqlCommand(insertLengthQuery, connection))
                {
                    lengthCmd.Parameters.Add("@StartDate", System.Data.SqlDbType.Date).Value = startDate;
                    lengthCmd.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int).Value = projectId;
                    lengthCmd.Parameters.Add("@EndDate", System.Data.SqlDbType.Date).Value = endDate;
                    lengthCmd.ExecuteNonQuery();
                }
                  
                
            }
            Enum.TryParse<Complexity>(complexity, out Complexity complexityEnum);

            Project project = (new Project
            {
                Id = projectId,
                ProjectName = projectName,
                ComplexityEnum = complexityEnum,
                StartDate = startDate,
                EndDate = endDate,
                Phase = new Phase { phaseStage = PhaseStage.Installation, Lenght = DateTime.Now }
            });
            
            projects.Add(project);

            return project;
        }
        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT Project.*, Project_Length.StartDate, Project_Length.EndDate, PROJECT_COORDINATOR.Initials, PROJECT_COORDINATOR.CoordinatorId
                    FROM Project 
                    INNER JOIN Project_Length ON Project.ProjectID = Project_Length.ProjectID
                    LEFT JOIN PROJECT_COORDINATOR ON Project.CoordinatorId = PROJECT_COORDINATOR.CoordinatorId", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Enum.TryParse<Complexity>((string)reader["Complexity"], out Complexity complexityEnum);

                        Project project = new Project()
                        {
                            Id = reader.GetInt32(0),
                            ProjectName = (string)reader["ProjectName"],
                            StartDate = DateOnly.FromDateTime((DateTime)reader["StartDate"]),
                            EndDate = DateOnly.FromDateTime((DateTime)reader["EndDate"]),
                            ComplexityEnum = complexityEnum,
                            ProjectCoordinator = new ProjectCoordinator 
                            { 
                                CoordinatorId = reader["CoordinatorId"] != DBNull.Value ? (int)reader["CoordinatorId"] : 0,
                                Initials = reader["Initials"] != DBNull.Value ? (string)reader["Initials"] : string.Empty
                            },
                            Phase = new Phase { phaseStage = PhaseStage.Installation, Lenght = DateTime.Now }
                        };
                        projects.Add(project);
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
