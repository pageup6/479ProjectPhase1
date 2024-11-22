using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        // Navigation property
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}