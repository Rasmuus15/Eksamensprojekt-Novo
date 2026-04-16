using System;
using System.Collections.Generic;
using System.Text;

namespace NovoForecastingSystem.Repos
{
    public class RessourceRepo
    {
        private List<Models.Ressource> ressource;

        public RessourceRepo()
        {
            ressource = new List<Models.Ressource>(); 
        }


         public List<Models.Ressource> GetAllRessources()
        {
            return ressource;
        }
    }
}
