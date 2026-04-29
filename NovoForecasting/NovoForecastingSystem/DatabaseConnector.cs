using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Data;
using Microsoft.Data.SqlClient;


namespace NovoForecastingSystem
{
    public abstract class DatabaseConnector
    {
        protected readonly string connectionString;

        public DatabaseConnector()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = config.GetConnectionString("MyDBConnection");
        }
    }
}
