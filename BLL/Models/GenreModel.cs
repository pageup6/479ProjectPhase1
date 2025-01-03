using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models

{
    public class GenreModel
    {
        public Genre Record { get; set; }
        [DisplayName("Name")]
        public string Name => Record.Name;



    }
}
