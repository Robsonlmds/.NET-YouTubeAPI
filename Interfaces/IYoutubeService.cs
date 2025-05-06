using FiespYouTubeAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FiespYouTubeAPI.Interfaces
{
    public interface IYoutubeService
    {
        Task<YoutubeChannelDto?> GetChannelInfoAsync(string username);
    }
}
