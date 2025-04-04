namespace CouchPick.Server.Data.Models;

public class MovieSearchResult
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Overview { get; set; } = "";
    public string PosterPath { get; set; } = "";
    public string ReleaseDate { get; set; } = "";
}
