using Microsoft.AspNetCore.Mvc;

namespace FiespYouTubeAPI.Settings
{
    public class AppSettings
    {
        public InfosContent InfosContent { get; set; } = new();
    }

    public class InfosContent
    {
        public string ApiKey { get; set; } = string.Empty;
    }

}
