using CouchPick.Server.Data.Models;
using CouchPick.Server.Data.Entities;
using System.Text.Json;

namespace CouchPick.Api.Services;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public TmdbService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<MovieSearchResult>> SearchMoviesAsync(string query)
    {
        var token = _config["Tmdb:BearerToken"];
        var url = $"{_config["Tmdb:BaseUrl"]}/search/movie?query={Uri.EscapeDataString(query)}&include_adult=false&language=en-US&page=1";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        var results = doc.RootElement.GetProperty("results");

        return results.EnumerateArray()
            .Select(item => new MovieSearchResult
            {
                Id = item.GetProperty("id").GetInt32(),
                Title = item.GetProperty("title").GetString() ?? "",
                Overview = item.GetProperty("overview").GetString() ?? "",
                PosterPath = item.GetProperty("poster_path").GetString() ?? "",
                ReleaseDate = item.GetProperty("release_date").GetString() ?? ""
            }).ToList();
    }


    public async Task<List<Genre>> GetGenresAsync()
    {
        var token = _config["Tmdb:BearerToken"];
        var url = $"{_config["Tmdb:BaseUrl"]}/genre/movie/list?language=en";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        return doc.RootElement.GetProperty("genres").EnumerateArray()
            .Select(g => new Genre
            {
                Id = g.GetProperty("id").GetInt32(),
                Name = g.GetProperty("name").GetString() ?? ""
            }).ToList();
    }

}