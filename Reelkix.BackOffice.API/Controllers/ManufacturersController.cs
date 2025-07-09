using Microsoft.AspNetCore.Mvc;
using Reelkix.BackOffice.Application.Manufacturers.Commands.CreateManufacturer;
using Reelkix.BackOffice.Application.Manufacturers.Queries.GetAllManufacturers;

namespace Reelkix.BackOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly CreateManufacturerHandler _createHandler;
        private readonly GetAllManufacturersHandler _getAllHandler;

        public ManufacturersController(CreateManufacturerHandler createHandler, GetAllManufacturersHandler getAllHandler)
        {
            _createHandler = createHandler ?? throw new ArgumentNullException(nameof(createHandler));
            _getAllHandler = getAllHandler ?? throw new ArgumentNullException(nameof(getAllHandler));
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
    }
}
