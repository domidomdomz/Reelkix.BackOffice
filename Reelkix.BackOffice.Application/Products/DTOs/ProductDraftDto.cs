namespace Reelkix.BackOffice.Application.Products.DTOs
{
    public class ProductDraftDto
    {
        public string Name { get; set; } = string.Empty; // The name of the product draft, cannot be null or empty.
        public string Description { get; set; } = string.Empty; // The description of the product draft, cannot be null or empty.
        public decimal CostPrice { get; set; } = 0; // The cost price of the product draft, defaulting to 0.
        public decimal SellingPrice { get; set; } = 0; // The selling price of the product draft, defaulting to 0.
        public Guid ManufacturerId { get; set; } = Guid.Empty; // The unique identifier of the manufacturer associated with the product draft, cannot be null or empty.
    }
}
