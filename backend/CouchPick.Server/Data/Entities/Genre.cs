using System.ComponentModel.DataAnnotations;

namespace CouchPick.Server.Data.Entities;

public class Genre
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
}
