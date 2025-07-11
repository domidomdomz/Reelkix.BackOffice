using Microsoft.AspNetCore.Mvc;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.Queries.GetAllProducts;
using Reelkix.BackOffice.Application.Products.Queries.GetProductById;

namespace Reelkix.BackOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CreateProductHandler _createHandler;
        private readonly GetProductByIdHandler _getHandler;
        private readonly GetAllProductsHandler _getAllHandler;

        public ProductsController(CreateProductHandler createHandler, GetProductByIdHandler getHandler, GetAllProductsHandler getAllHandler)
        {
            _createHandler = createHandler ?? throw new ArgumentNullException(nameof(createHandler));
            _getHandler = getHandler ?? throw new ArgumentNullException(nameof(getHandler));
            _getAllHandler = getAllHandler ?? throw new ArgumentNullException(nameof(getAllHandler));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
                return BadRequest("Invalid product data.");

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

        [HttpGet]
        public IActionResult GetAllProducts(CancellationToken cancellation)
        {
            var products = _getAllHandler.Handle(cancellation).Result;
            if (products == null || !products.Any())
                return NotFound("No products found.");
            return Ok(products);
        }
    }
}
