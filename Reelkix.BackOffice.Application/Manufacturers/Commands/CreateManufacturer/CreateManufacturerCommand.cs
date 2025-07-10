namespace Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer
{
    public class CreateManufacturerCommand
    {
        public string Name { get; set; } = default!; // The name of the manufacturer, cannot be null or empty.
        public string Description { get; set; } = default!; // A detailed description of the manufacturer, cannot be null or empty.

        // Additional properties can be added here as needed
        // For example, you might want to include a logo URL or contact information
    }
}
