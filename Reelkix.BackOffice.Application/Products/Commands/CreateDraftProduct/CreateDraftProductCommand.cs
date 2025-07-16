namespace Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct
{
    public class CreateDraftProductCommand
    {
        public String Name { get; set; } = String.Empty; // The name of the product, cannot be null or empty.
        public String Description { get; set; } = String.Empty; // The description of the product, cannot be null or empty.
        public decimal CostPrice { get; set; } = 0; // The cost price of the product, defaulting to 0.
        public decimal SellingPrice { get; set; } = 0; // The selling price of the product, defaulting to 0.
        public Guid ManufacturerId { get; set; } = Guid.Empty; // The unique identifier of the manufacturer, cannot be null or empty.
    }
}
