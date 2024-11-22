using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}