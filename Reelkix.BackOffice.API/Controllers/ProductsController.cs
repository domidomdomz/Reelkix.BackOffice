﻿using Microsoft.AspNetCore.Mvc;
using Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct;
using Reelkix.BackOffice.Application.Products.Commands.CreateProduct;
using Reelkix.BackOffice.Application.Products.Commands.DeleteDraftProduct;
using Reelkix.BackOffice.Application.Products.Commands.UpdateDraftProduct;
using Reelkix.BackOffice.Application.Products.Commands.UpdateProduct;
using Reelkix.BackOffice.Application.Products.Queries.GetAllProducts;
using Reelkix.BackOffice.Application.Products.Queries.GetDraftProductById;
using Reelkix.BackOffice.Application.Products.Queries.GetProductById;

namespace Reelkix.BackOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CreateProductHandler _createHandler;
        private readonly CreateDraftProductHandler _createDraftHandler;
        private readonly UpdateProductHandler _updateHandler;
        private readonly UpdateDraftProductHandler _updateDraftHandler;
        private readonly GetProductByIdHandler _getHandler;
        private readonly GetDraftProductByIdHandler _getDraftHandler;
        private readonly GetAllProductsHandler _getAllHandler;
        private readonly DeleteDraftProductHandler _deleteDraftProductHandler;

        public ProductsController(
            CreateProductHandler createHandler,
            CreateDraftProductHandler createDraftHandler,
            UpdateProductHandler updateHandler,
            UpdateDraftProductHandler updateDraftHandler,
            GetProductByIdHandler getHandler,
            GetDraftProductByIdHandler getDraftHandler,
            GetAllProductsHandler getAllHandler,
            DeleteDraftProductHandler deleteDraftProductHandler)
        {
            _createHandler = createHandler ?? throw new ArgumentNullException(nameof(createHandler));
            _createDraftHandler = createDraftHandler ?? throw new ArgumentNullException(nameof(createDraftHandler));
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
            _updateDraftHandler = updateDraftHandler ?? throw new ArgumentNullException(nameof(updateDraftHandler));
            _getHandler = getHandler ?? throw new ArgumentNullException(nameof(getHandler));
            _getDraftHandler = getDraftHandler ?? throw new ArgumentNullException(nameof(getDraftHandler));
            _getAllHandler = getAllHandler ?? throw new ArgumentNullException(nameof(getAllHandler));
            _deleteDraftProductHandler = deleteDraftProductHandler ?? throw new ArgumentNullException(nameof(deleteDraftProductHandler));
        }

        [HttpGet("draft/{id}")]
        public async Task<IActionResult> GetDraftProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _getDraftHandler.Handle(new GetDraftProductByIdQuery(id), cancellationToken);
            if (product == null) return NotFound($"Draft product with ID {id} not found.");
            return Ok(product);
        }

        [HttpPost("draft")]
        public async Task<IActionResult> CreateDraftProduct([FromBody] CreateDraftProductCommand command, CancellationToken cancellationToken)
        {
            var draftId = await _createDraftHandler.Handle(command, cancellationToken);
            return Ok(new { productId = draftId });
        }

        [HttpPut("draft/{id}")]
        public async Task<IActionResult> UpdateDraftProduct(Guid id, [FromBody] UpdateDraftProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null || command.ProductId != id)
                return BadRequest("Invalid draft product data or ID mismatch.");
            
            await _updateDraftHandler.Handle(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("draft/{id}")]
        public async Task<IActionResult> DeleteDraftProduct(Guid id, CancellationToken cancellationToken)
        {
            await _deleteDraftProductHandler.Handle(id, cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (command == null || command.ProductId != id)
                return BadRequest("Invalid product data or ID mismatch.");
            
            await _updateHandler.Handle(command, cancellationToken);
            return NoContent();
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
            if (products == null)
                return NotFound("No products found.");
            if (!products.Any())
                return NoContent();
            return Ok(products);
        }
    }
}
