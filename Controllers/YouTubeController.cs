using FiespYouTubeAPI.Dtos;
using FiespYouTubeAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiespYouTubeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YoutubeController : ControllerBase
    {
        #region Fields

        private readonly IYoutubeService _youtubeService;

        #endregion

        #region Constructor

        public YoutubeController(IYoutubeService youtubeService)
        {
            _youtubeService = youtubeService;
        }

        #endregion

        #region Endpoints

        [HttpGet("{username}")]
        public async Task<IActionResult> GetChannel(string username)
        {
            var channelInfo = await _youtubeService.GetChannelInfoAsync(username);
            if (channelInfo == null)
                return NotFound("Canal não encontrado.");

            return Ok(channelInfo);
        }

        #endregion
    }
}
