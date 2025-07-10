namespace Reelkix.BackOffice.Application.Manufacturers.DTOs
{
    public class ManufacturerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!; // Name of the manufacturer, must be set before use
        public string Description { get; set; } = default!; // Description of the manufacturer, must be set before use
        public DateTime CreatedAt { get; set; } // Date and time when the manufacturer was created
        public DateTime UpdatedAt { get; set; } // Date and time when the manufacturer was last updated

    }
}
