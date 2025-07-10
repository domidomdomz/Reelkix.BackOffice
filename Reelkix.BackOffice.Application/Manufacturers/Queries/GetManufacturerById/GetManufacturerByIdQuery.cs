namespace Reelkix.BackOffice.Application.Manufacturers.Queries.GetManufacturerById
{
    public class GetManufacturerByIdQuery
    {
        public Guid Id { get; set; } // Unique identifier for the manufacturer to be retrieved. This is the ID of the manufacturer being queried.
        public GetManufacturerByIdQuery(Guid id)
        {
            Id = id; // Constructor to initialize the query with a specific manufacturer ID.
        }
    }
}
