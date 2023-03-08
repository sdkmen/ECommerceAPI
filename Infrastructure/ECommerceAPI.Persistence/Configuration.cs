using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get {
                ConfigurationManager configurationManager = new();
                //appsettings.json'a gitmek için path oluşturuyoruz, Directory.GetCurrentDirectory() bize içinde bulunduğumuz ECommerceAPI.Persistence klasörünü veriyor ../ ile bir dışa Infrastructure'a çıkıyoruz ../ ile solutiona ulaşıp devamında ECommerceAPI.API'ye giriyoruz.
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API"));
                //Microsoft.Extensions.Configurations.Json kütüphanesi sayesinde addJsonFile'a ulaşabiliyoruz.
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("PostgreSQL");
            }    
        }
    }
}
