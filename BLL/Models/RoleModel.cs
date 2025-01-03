using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class RoleModel
    {
        // Underlying data entity
        public Role Record { get; set; }

        // Properties to expose relevant data
        public int Id => Record?.Id ?? 0; // Default to 0 if Record is null
        public string Name => Record?.Name ?? "UnknownRole"; // Default to "UnknownRole" if null

        // Optional: expose the user count if required
        public int UserCount => Record?.Users?.Count ?? 0;

        // Constructor to initialize with a Role entity
        public RoleModel(Role record)
        {
            Record = record ?? throw new ArgumentNullException(nameof(record), "Role cannot be null.");
        }

        // Parameterless constructor for flexibility in serialization scenarios (optional)
        public RoleModel() { }
    }
}
