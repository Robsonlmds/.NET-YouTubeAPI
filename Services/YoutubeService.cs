using FiespYouTubeAPI.Dtos;
using FiespYouTubeAPI.Interfaces;
using FiespYouTubeAPI.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;

namespace FiespYouTubeAPI.Services
{
    public class YoutubeService : IYoutubeService
    {
        #region Fields

        private readonly AppSettings _appSettings;
        private readonly YouTubeService _youtubeService;

        #endregion

        #region Constructor

        public YoutubeService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            _youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = _appSettings.InfosContent.ApiKey,
                ApplicationName = "FiespYouTubeAPI"
            });
        }

        #endregion

        #region Public Methods

        public async Task<YoutubeChannelDto?> GetChannelInfoAsync(string username)
        {
            var channel = await ObterCanalPorNomeAsync(username);
            if (channel is null)
                return null;

            var videos = await ObterUltimosVideosDoCanalAsync(channel.CanalId);

            return new YoutubeChannelDto
            {
                NomeCanal = channel.NomeCanal,
                Descricao = channel.Descricao,
                CanalId = channel.CanalId,
                UltimosVideos = videos
            };
        }

        #endregion

        #region Private Methods

        private async Task<YoutubeChannelDto?> ObterCanalPorNomeAsync(string username)
        {
            var searchRequest = _youtubeService.Search.List("snippet");
            searchRequest.Q = username;
            searchRequest.Type = "channel";
            searchRequest.MaxResults = 1;

            var response = await searchRequest.ExecuteAsync();
            var item = response.Items.FirstOrDefault();
            if (item == null) return null;

            return new YoutubeChannelDto
            {
                NomeCanal = item.Snippet.Title,
                Descricao = item.Snippet.Description,
                CanalId = item.Snippet.ChannelId
            };
        }

        private async Task<List<YoutubeVideoDto>> ObterUltimosVideosDoCanalAsync(string canalId)
        {
            var searchRequest = _youtubeService.Search.List("snippet");
            searchRequest.ChannelId = canalId;
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchRequest.MaxResults = 5;
            searchRequest.Type = "video";

            var response = await searchRequest.ExecuteAsync();
            var videoIds = response.Items.Select(i => i.Id.VideoId).Where(id => !string.IsNullOrEmpty(id)).ToList();
            if (!videoIds.Any()) return new();

            var videosRequest = _youtubeService.Videos.List("snippet,statistics");
            videosRequest.Id = string.Join(",", videoIds);

            var videosResponse = await videosRequest.ExecuteAsync();

            return videosResponse.Items.Select(v => new YoutubeVideoDto
            {
                Titulo = v.Snippet.Title,
                ThumbnailUrl = v.Snippet.Thumbnails.Medium?.Url ?? "",
                PublicadoEm = v.Snippet.PublishedAt?.ToString("yyyy-MM-ddTHH:mm:ss") ?? "",
                Visualizacoes = v.Statistics.ViewCount,
                Likes = v.Statistics.LikeCount,
                Comentarios = v.Statistics.CommentCount,
                VideoId = v.Id
            }).ToList();
        }

        #endregion
    }
}
