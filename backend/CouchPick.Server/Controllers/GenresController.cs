using CouchPick.Api.Services;
using CouchPick.Server.Data;
using CouchPick.Server.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouchPick.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly TmdbService _tmdb;

    public GenresController(ApplicationDbContext db, TmdbService tmdb)
    {
        _db = db;
        _tmdb = tmdb;
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncGenres()
    {
        var genres = await _tmdb.GetGenresAsync();

        foreach (var genre in genres)
        {
            var existing = await _db.Genres.FindAsync(genre.Id);
            if (existing == null)
            {
                _db.Genres.Add(genre);
            }
            else
            {
                existing.Name = genre.Name;
            }
        }

        await _db.SaveChangesAsync();
        return Ok(new { count = genres.Count });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _db.Genres.ToListAsync();
        return Ok(genres);
    }
}
