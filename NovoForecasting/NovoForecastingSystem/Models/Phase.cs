using System;
using System.Collections.Generic;
using System.Text;
using PhaseEnum = NovoForecastingSystem.Models.Enums.PhaseStage;


namespace NovoForecastingSystem.Models
{
    public class Phase
    {
        public DateTime Length { get; set; }
        public PhaseEnum phaseStage { get; set; }

        public string DisplayPhaseStage
        {
            get
            {
                int currentPhase = (int)phaseStage + 1;
                return $"{currentPhase}/8";
            }
        }
    }
}
