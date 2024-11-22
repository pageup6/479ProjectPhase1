using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class DirectorModel
    {
        public Director Record { get; set; }

        [DisplayName("Name")]
        public string Name => Record.Name;
        public string Surname => Record.Surname;
        [DisplayName("Release Date")]

        public string IsRetired => Record.IsRetired ? "Yes" : "No";

    }
}
