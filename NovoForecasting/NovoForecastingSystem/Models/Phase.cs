using System;
using System.Collections.Generic;
using System.Text;
using PhaseEnum = NovoForecastingSystem.Models.Enums.PhaseStage;


namespace NovoForecastingSystem.Models
{
    public class Phase
    {
        public DateTime Lenght { get; set; }
        public PhaseEnum phaseEnum { get; set; }
    }
}
 