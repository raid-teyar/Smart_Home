using Microsoft.AspNetCore.Hosting.Server;

namespace SecuritySystem.Server
{
    public static class Globals
    {
        public static string ConnectionString { get; set; } = "server=localhost;user=;database=smart home;port=3306;password=";
    }
}
