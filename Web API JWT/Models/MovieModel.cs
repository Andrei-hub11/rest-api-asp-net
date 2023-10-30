using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API_JWT.Models;


[Table("Movies")]
public class MovieModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public string Category { get; set; }
    public int Year { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
