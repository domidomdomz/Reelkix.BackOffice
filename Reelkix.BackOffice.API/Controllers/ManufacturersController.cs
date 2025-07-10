using Microsoft.AspNetCore.Mvc;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetAllManufacturers;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetManufacturerById;

namespace Reelkix.BackOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly CreateManufacturerHandler _createHandler;
        private readonly GetAllManufacturersHandler _getAllHandler;
        private readonly GetManufacturerByIdHandler _getByIdHandler;

        public ManufacturersController(CreateManufacturerHandler createHandler, GetAllManufacturersHandler getAllHandler, GetManufacturerByIdHandler getByIdHandler)
        {
            _createHandler = createHandler ?? throw new ArgumentNullException(nameof(createHandler));
            _getAllHandler = getAllHandler ?? throw new ArgumentNullException(nameof(getAllHandler));
            _getByIdHandler = getByIdHandler ?? throw new ArgumentNullException(nameof(getByIdHandler));
        }

        [HttpPost]
        public async Task<IActionResult> CreateManufacturer([FromBody] CreateManufacturerCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
                return BadRequest("Invalid manufacturer data.");
            var manufacturerId = await _createHandler.Handle(command, cancellationToken);
            return CreatedAtAction(nameof(GetAllManufacturers), new { id = manufacturerId }, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManufacturers(CancellationToken cancellationToken)
        {
            var manufacturers = await _getAllHandler.Handle(cancellationToken);
            if (manufacturers == null || !manufacturers.Any())
                return NotFound("No manufacturers found.");
            return Ok(manufacturers);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetManufacturerById(Guid id, CancellationToken cancellationToken)
        {
            var manufacturer = await _getByIdHandler.Handle(new GetManufacturerByIdQuery(id), cancellationToken);
            if (manufacturer == null)
                return NotFound($"Manufacturer with ID {id} not found.");
            return Ok(manufacturer);
        }
    }
}
