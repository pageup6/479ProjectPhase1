using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Surname { get; set; }

        public bool IsRetired { get; set; }

        // Navigation property
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}