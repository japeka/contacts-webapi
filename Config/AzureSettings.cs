using System.IO;
using Microsoft.Extensions.Configuration;

namespace ContactsWebApi.Config
{
    public class AzureSettings
    {
        public string DirectoryId { get; set; }
        public string ApplicationId { get; set; }
        public string Resource { get; set; }
        public string GrantType { get; set; }
        public string Key { get; set; }
        public string EndPoint { get; set; }
        public IConfiguration Configuration { get; set; }

        public AzureSettings()
        {
            var builder = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            this.DirectoryId = Configuration["AzureSettings:DirectoryId"];
            this.ApplicationId = Configuration["AzureSettings:ApplicationId"];
            this.Resource = Configuration["AzureSettings:Resource"];
            this.GrantType = Configuration["AzureSettings:GrantType"];
            this.Key = Configuration["AzureSettings:Key"];
            this.EndPoint = "https://login.windows.net/"+ this.DirectoryId + "/oauth2/token";
        }

    }
}
