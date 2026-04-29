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

        private DateOnly _endDate;
        public DateOnly EndDate
        {
            get
            {
                if (_endDate == DateOnly.MinValue && StartDate != DateOnly.MinValue)
                {
                    if (ComplexityEnum == ComplexityEnum.Low) return StartDate.AddDays(81 * 7);
                    else if (ComplexityEnum == ComplexityEnum.Medium) return StartDate.AddDays(108 * 7);
                    else if (ComplexityEnum == ComplexityEnum.High) return StartDate.AddDays(137 * 7);
                }
                return _endDate;
            }
            set { _endDate = value; }
        }

        public ComplexityEnum ComplexityEnum { get; set; }
        public Phase Phase { get; set; }
        private JobRoleEnum jobRoleEnum;
       public ProjectCoordinator projectCoordinator   { get; set; }
    }
}
