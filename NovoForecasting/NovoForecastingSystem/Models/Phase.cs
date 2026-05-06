using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using PhaseEnum = NovoForecastingSystem.Models.Enums.PhaseStage;
using NovoForecastingSystem.Models.Enums;
using System.Numerics;


namespace NovoForecastingSystem.Models
{
    public class Phase
    {
        public DateTime Length { get; set; }
        public PhaseEnum phaseStage { get; set; }

        public int currentPhase;


        public Complexity Complexity;

        public string DisplayPhaseStage
        {
            get
            {
                currentPhase = (int)phaseStage + 1;
                return $"{currentPhase}/8";
            }
        }

        public int DisplayComboBox
        {
            get
            {
                return currentPhase;
            }
        }

        public static PhaseEnum ReturnPhase(Complexity complexcity, int Days)
        {
            switch (complexcity)
            {
                case Complexity.Low:
                    if (Days <= 35)
                    {
                        return PhaseEnum.ConceptDesign;
                    }
                    else if (Days <= 91)
                    {
                        return PhaseEnum.BasicDesign;
                    }

                    else if (Days <= 147)
                    {
                        return PhaseEnum.DetailedDesign;
                    }

                    else if (Days <= 364)
                    {
                        return PhaseEnum.Manufacturing;
                    }

                    else if (Days <= 399)
                    {
                        return PhaseEnum.Installation;
                    }
                    else if (Days <= 434)
                    {
                        return PhaseEnum.Testing;
                    }
                    else if (Days <= 532)
                    {
                        return PhaseEnum.Approval;
                    }
                    else
                    {
                        return PhaseEnum.GoLive;
                    }


                case Complexity.Medium:
                    if (Days <= 49)
                    {
                        return PhaseEnum.ConceptDesign;
                    }
                    else if (Days <= 119)
                    {
                        return PhaseEnum.BasicDesign;
                    }

                    else if (Days <= 189)
                    {
                        return PhaseEnum.DetailedDesign;
                    }

                    else if (Days <= 483)
                    {
                        return PhaseEnum.Manufacturing;
                    }

                    else if (Days <= 525)
                    {
                        return PhaseEnum.Installation;
                    }

                    else if (Days <= 574)
                    {
                        return PhaseEnum.Testing;
                    }

                    else if (Days <= 707)
                    {
                        return PhaseEnum.Approval;
                    }

                    else
                    {
                        return PhaseEnum.GoLive;
                    }


                case Complexity.High:
                    if (Days <= 63)
                    {
                        return PhaseEnum.ConceptDesign;
                    }
                    else if (Days <= 154)
                    {
                        return PhaseEnum.BasicDesign;
                    }
                    else if (Days <= 245)
                    {
                        return PhaseEnum.DetailedDesign;
                    }

                    else if (Days <= 609)
                    {
                        return PhaseEnum.Manufacturing;
                    }

                    else if (Days <= 665)
                    {
                        return PhaseEnum.Installation;
                    }

                    else if (Days <= 727)
                    {
                        return PhaseEnum.Testing;
                    }

                    else if (Days <= 895)
                    {
                        return PhaseEnum.Approval;
                    }

                    else
                    {
                        return PhaseEnum.GoLive;
                    }
                
            }
            throw new Exception("Invalid complexity");
        }
    }
}
