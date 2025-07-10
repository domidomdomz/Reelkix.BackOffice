namespace Reelkix.BackOffice.Application.Manufacturers.Commands.UpdateManufacturer
{
    public class UpdateManufacturerCommand
    {
        public Guid Id { get; set; } // Unique identifier for the manufacturer to be updated. This is the ID of the manufacturer being modified.
        public string Name { get; set; } = default!; // The name of the manufacturer, cannot be null or empty.
        public string Description { get; set; } = default!; // A detailed description of the manufacturer, cannot be null or empty.
        
    }
}
