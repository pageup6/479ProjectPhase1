using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class MovieGenreModel
    {
        public MovieGenre Record { get; set; }

        [DisplayName("Movie")]
        public string MovieName => Record.Movie?.Name;

        [DisplayName("Genre")]
        public string GenreName => Record.Genre?.Name;

        [DisplayName("Movie ID")]
        public int MovieId => Record.MovieId;

        [DisplayName("Genre ID")]
        public int GenreId => Record.GenreId;
    }
}
