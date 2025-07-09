namespace Reelkix.BackOffice.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery
    {
        public Guid Id { get; set; } // Unique identifier for the product to be retrieved. This is the ID of the product being queried.

        public GetProductByIdQuery(Guid id)
        {
            Id = id; // Constructor to initialize the query with a specific product ID.
        }
    }
}
