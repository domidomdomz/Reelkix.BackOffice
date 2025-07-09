using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.Queries.CreateProductById;

namespace Reelkix.BackOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CreateProductHandler _createHandler;
        private readonly GetProductByIdHandler _getHandler;

        public ProductsController(CreateProductHandler createHandler, GetProductByIdHandler getHandler)
        {
            _createHandler = createHandler ?? throw new ArgumentNullException(nameof(createHandler));
            _getHandler = getHandler ?? throw new ArgumentNullException(nameof(getHandler));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null) return BadRequest("Invalid product data.");
            var productId = await _createHandler.Handle(command, cancellationToken);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _getHandler.Handle(new GetProductByIdQuery(id), cancellationToken);
            if (product == null) return NotFound($"Product with ID {id} not found.");
            return Ok(product);
        }
    }
}
