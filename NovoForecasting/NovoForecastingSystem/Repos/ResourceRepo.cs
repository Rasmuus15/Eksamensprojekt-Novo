using System;
using System.Collections.Generic;
using System.Text;

namespace NovoForecastingSystem.Repos
{
    public class ResourceRepo
    {
        private List<Models.Ressource> ressource;

        public ResourceRepo()
        {
            ressource = new List<Models.Ressource>(); 
        }


         public List<Models.Ressource> GetAllRessources()
        {
            return ressource;
        }
    }
}
