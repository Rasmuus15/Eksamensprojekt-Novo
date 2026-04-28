using System;
using System.Collections.Generic;
using System.Text;
using JobRoleEnum = NovoForecastingSystem.Models.Enums.JobRole;
using ComplexityEnum = NovoForecastingSystem.Models.Enums.Complexity;

namespace NovoForecastingSystem.Models
{
    public class Project
    {
        //public int projectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private JobRoleEnum jobRoleEnum;
        private ComplexityEnum complexityEnum;
    }
}
