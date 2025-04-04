using CouchPick.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CouchPick.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly TmdbService _tmdb;

    public SearchController(TmdbService tmdb)
    {
        _tmdb = tmdb;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Query is required.");

        var results = await _tmdb.SearchMoviesAsync(query);
        return Ok(results);
    }
}
