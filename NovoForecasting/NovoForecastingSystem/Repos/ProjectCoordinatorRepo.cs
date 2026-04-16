using System;
using System.Collections.Generic;
using System.Text;

namespace NovoForecastingSystem.Repos
{
    public class ProjectCoordinatorRepo
    {
        private List<Models.ProjectCoordinator> projectCoordinators;
        public ProjectCoordinatorRepo()
        {
            projectCoordinators = new List<Models.ProjectCoordinator>();
        }
        public List<Models.ProjectCoordinator> GetAllProjectCoordinators()
        {
            return projectCoordinators;
        }
    }
}
