using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Persistence
{
    static class Configuration
    {
        //static public string ConnectionString
        //{
        //    get
        //    {
        //        ConfigurationManager configurationManager = new();
        //        configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/NewCommerce.Api"));
        //        configurationManager.AddJsonFile("appsettings.json");
        //        return configurationManager.GetConnectionString("PsotgreSql");
        //    }
        //}
        static public string ConnectionString
        {
            get
            {
                var configurationManager = new ConfigurationManager();
                configurationManager.SetBasePath(Directory.GetCurrentDirectory());
                configurationManager.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                var connectionString = configurationManager.GetConnectionString("PostgreSql");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Connection string 'PostgreSql' not found in appsettings.json.");
                }
                return connectionString;
            }
        }

    }
}
