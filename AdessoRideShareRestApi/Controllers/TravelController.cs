using AdessoRideShareRestApi.Infrastructure;
using AdessoRideShareRestApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShareRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly ITravelService _travelService;
        private readonly IMapper _mapper;

        public TravelController(ITravelService travelService, IMapper mapper)
        {
            _travelService = travelService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var travels = await _travelService.Get();
            var travelsModel = _mapper.Map<IEnumerable<TravelModel>>(travels);
            return Ok(travelsModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var travel = await _travelService.Get(id);
            if (travel == null)
            {
                return NotFound("Travel not found");
            }
            var travelModel = _mapper.Map<TravelModel>(travel);
            return Ok(travelModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TravelModel travelModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var travel = _mapper.Map<TravelModel>(travelModel);
            await _travelService.Create(travel);
            travelModel = _mapper.Map<TravelModel>(travel);
            return CreatedAtAction(nameof(Get), new { id = travelModel.TravelId }, travelModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TravelModel travelModel)
        {
            if (await _travelService.Validate(id) == false)
            {
                return NotFound("Travel not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != travelModel.TravelId)
            {
                return BadRequest();
            }

            var travel = _mapper.Map<TravelModel>(travelModel);
            await _travelService.Update(travel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _travelService.Validate(id) == false)
            {
                return NotFound("Travel not found");
            }

            await _travelService.Delete(id);
            return NoContent();
        }
    }
}
