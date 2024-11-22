using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class MovieModel
    {
        public Movie Record { get; set; }
        [DisplayName("Name")]
        public string Name => Record.Name;
        [DisplayName("Release Date")]
        public string ReleaseDate => Record.ReleaseDate is null ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy HH:mm:ss");

        public string TotalRevenue => Record.TotalRevenue.ToString("N2");

        public string Director => Record.Director?.Name;


    }
}
