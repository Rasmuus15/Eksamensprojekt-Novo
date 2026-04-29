using System;
using System.Collections.Generic;
using System.Text;
using JobRoleEnum = NovoForecastingSystem.Models.Enums.JobRole;
using ComplexityEnum = NovoForecastingSystem.Models.Enums.Complexity;

namespace NovoForecastingSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ComplexityEnum ComplexityEnum { get; set; }
        private JobRoleEnum jobRoleEnum;
        public ProjectCoordinator ProjectCoordinator { get; set; } = new ProjectCoordinator();
        
    }
}
