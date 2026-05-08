using NovoForecastingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovoForecastingSystem.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public double FTE { get; set; }
        public bool Availability { get; set; }
        public JobRole JobRoleEnum { get; set; }
        public int? ProjectId { get; set; }
    }
}
