using GuardianNewsApp.Application.Interfaces;
using GuardianNewsApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GuardianNewsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardianApiController : ControllerBase
    {
        private readonly IGuardianApiService _guardianApiService;

        public GuardianApiController(IGuardianApiService guardianApiService)
        {
            _guardianApiService = guardianApiService;
        }

        [HttpGet("news")]
        public async Task<ActionResult<List<News>>> GetNews()
        {
            try
            {
                var newsArticles = await _guardianApiService.FetchNewsFromGuardianApiAsync();
                if (newsArticles == null || newsArticles.Count == 0)
                {
                    return NotFound("No news found.");
                }

                return Ok(newsArticles);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search{searchInput}")]
        public async Task<ActionResult<List<News>>> GetNewsBySearchInput( string searchInput)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchInput))
                {
                    return BadRequest("Search input cannot be empty.");
                }

                var newsArticles = await _guardianApiService.FetchNewsBySearchInput(searchInput);
                if (newsArticles == null || newsArticles.Count == 0)
                {
                    return NotFound("No news found for the given search input.");
                }

                return Ok(newsArticles);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
