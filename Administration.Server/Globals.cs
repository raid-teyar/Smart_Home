using SmartHome.Contracts.Models;

namespace SecuritySystem.Server
{
    public static class Globals
    {
        // get connection string from appsettings.json
        public static string ConnectionString { get; set; } = "server=localhost;user=;database=smart home;port=3306;password=";
        public static User? LoggedUser { get; set; }
    }
}
