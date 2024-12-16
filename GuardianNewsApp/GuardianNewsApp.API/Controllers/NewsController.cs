using GuardianNewsApp.Application.Services;
using GuardianNewsApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GuardianNewsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            var newsList = await _newsService.GetAllNewsAsync();
            return Ok(newsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNewsById(string id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<News>> AddNews(News news)
        {
            await _newsService.AddNewsAsync(news);
            return CreatedAtAction(nameof(GetNewsById), new { id = news.Id }, news);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(string id, News news)
        {
            if (id != news.Id)
            {
                return BadRequest();
            }
            await _newsService.UpdateNewsAsync(news);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(string id)
        {
            await _newsService.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}
