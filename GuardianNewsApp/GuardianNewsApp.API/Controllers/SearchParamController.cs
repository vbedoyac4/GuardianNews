using GuardianNewsApp.Application.Services;
using GuardianNewsApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GuardianNewsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchParamController : ControllerBase
    {
        private readonly SearchParamService _searchParamService;

        public SearchParamController(SearchParamService searchParamService)
        {
            _searchParamService = searchParamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchParam>>> GetSearchParams()
        {
            var searchParams = await _searchParamService.GetAllSearchParamsAsync();
            return Ok(searchParams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SearchParam>> GetSearchParamById(int id)
        {
            var searchParam = await _searchParamService.GetSearchParamByIdAsync(id);
            if (searchParam == null)
            {
                return NotFound();
            }
            return Ok(searchParam);
        }

        [HttpPost]
        public async Task<ActionResult<SearchParam>> AddSearchParam(SearchParam searchParam)
        {
            await _searchParamService.AddSearchParamAsync(searchParam);
            return CreatedAtAction(nameof(GetSearchParamById), new { id = searchParam.Id }, searchParam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSearchParam(int id, SearchParam searchParam)
        {
            if (id != searchParam.Id)
            {
                return BadRequest();
            }
            await _searchParamService.UpdateSearchParamAsync(searchParam);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearchParam(int id)
        {
            await _searchParamService.DeleteSearchParamAsync(id);
            return NoContent();
        }
    }
}
