using System;
using System.Collections.Generic;
using System.Text;
using PhaseEnum = NovoForecastingSystem.Models.Enums.Phase;


namespace NovoForecastingSystem.Models
{
    public class Phase
    {
        public DateTime Length { get; set; }
        private PhaseEnum phaseEnum;
    }
}
 