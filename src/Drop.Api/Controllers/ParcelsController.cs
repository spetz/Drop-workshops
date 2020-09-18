using System;
using System.Threading.Tasks;
using Drop.Application.Commands;
using Drop.Application.DTO;
using Drop.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParcelsController : ControllerBase
    {
        private readonly IParcelsService _parcelsService;

        public ParcelsController(IParcelsService parcelsService)
        {
            _parcelsService = parcelsService;
        }
    
        [HttpGet("{parcelId:guid}")]
        public async Task<ActionResult<ParcelDto>> Get(Guid parcelId)
        {
            var parcel = await _parcelsService.GetAsync(parcelId);
            if (parcel is null)
            {
                return NotFound();
            }

            return Ok(parcel);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AddParcel command)
        {
            await _parcelsService.AddAsync(command);
            return CreatedAtAction(nameof(Get), new {parcelId = command.Id}, null);
        }
    }
}