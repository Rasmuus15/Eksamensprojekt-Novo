using System;
using System.Collections.Generic;
using System.Text;

namespace NovoForecastingSystem.Repos
{
    public class ProjectRepo
    {
        private List<Models.Project> project;
        public ProjectRepo()
        {
            project = new List<Models.Project>();
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
