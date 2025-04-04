using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouchPick.Server.Data.Entities;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TmdbId { get; set; }

    [Required]
    public string Title { get; set; } = "";

    public string Description { get; set; } = "";
    public DateTime? ReleaseDate { get; set; }
    public string PosterUrl { get; set; } = "";
    public string BackdropUrl { get; set; } = "";
    public double VoteAverageTmdb { get; set; }
    public int? Runtime { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;


    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
}
