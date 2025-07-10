using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reelkix.BackOffice.SharedKernel;

namespace Reelkix.BackOffice.Domain.Manufacturers
{
    public class Manufacturer : IAuditable
    {
        public Guid Id { get; private set; } // Unique identifier for the manufacturer
        public string Name { get; set; } = default!; // Name of the manufacturer, must be set before use
        public string Description { get; set; } = default!; // Description of the manufacturer, must be set before use


        public DateTime CreatedAt { get; set; } // Timestamp when the manufacturer was created
        public DateTime UpdatedAt { get; set; } // Timestamp when the manufacturer was last updated

        private Manufacturer() { } // Private constructor for EF Core or other ORM usage

        public Manufacturer(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow; // Set the creation timestamp to the current UTC time
            UpdatedAt = DateTime.UtcNow; // Set the update timestamp to the current UTC time
        }
    }
}
