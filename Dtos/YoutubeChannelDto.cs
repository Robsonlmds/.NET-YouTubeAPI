using Microsoft.AspNetCore.Mvc;

namespace FiespYouTubeAPI.Dtos
{
    public class YoutubeChannelDto
    {
        public string NomeCanal { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string CanalId { get; set; } = string.Empty;
        public List<YoutubeVideoDto> UltimosVideos { get; set; } = new();
    }
}
