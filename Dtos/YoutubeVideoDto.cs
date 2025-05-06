using Microsoft.AspNetCore.Mvc;

namespace FiespYouTubeAPI.Dtos
{
    public class YoutubeVideoDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string PublicadoEm { get; set; } = string.Empty;
        public ulong? Visualizacoes { get; set; }
        public ulong? Likes { get; set; }
        public ulong? Comentarios { get; set; }
        public string VideoId { get; set; } = string.Empty;
        public string Url => $"https://www.youtube.com/watch?v={VideoId}";
    }

}
