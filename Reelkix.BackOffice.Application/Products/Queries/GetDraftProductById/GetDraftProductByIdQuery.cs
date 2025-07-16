using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reelkix.BackOffice.Application.Products.Queries.GetDraftProductById
{
    public class GetDraftProductByIdQuery
    {
        public Guid Id { get; set; } // Unique identifier for the draft product to be retrieved. This is the ID of the draft product being queried.
        public GetDraftProductByIdQuery(Guid id)
        {
            Id = id; // Constructor to initialize the query with a specific draft product ID.
        }
    }
}
