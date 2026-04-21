using System;
using System.Collections.Generic;
using System.Text;
using JobRoleEnum = NovoForecastingSystem.Models.Enums.JobRole;

namespace NovoForecastingSystem.Models
{
    public class Ressource
    {
        public string Initials { get; set; }
        public string Email { get; set; }
        public double FTE { get; set; }
        public bool Availability { get; set; }
        private JobRoleEnum jobRoleEnum;

    }
}
