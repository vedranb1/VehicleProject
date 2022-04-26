using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models;
using Project.Service.Models.DTOs;
using Project.Service.Repository;
using Project.Service.Repository.IRepository;
using Project.Service2.Models;
using System.Collections.Generic;

namespace Project.Service.Controllers
{
    [Route("api/v{version:apiVersion}/vehiclemake")]
    //[ApiExplorerSettings(GroupName = "VehicleProjectAPISpec")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {

        private readonly IVehicleMakeRepository _vmRepo;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleMakeRepository vmRepo, IMapper mapper)
        {
            _vmRepo = vmRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<VehicleMakeDTO>))]
        public IActionResult GetVehicleMakes([FromQuery] Filtering search, [FromQuery] Sorting sortBy, [FromQuery] Paging paging)
        {

            var vehicleMakesList = _vmRepo.GetVehicleMakes(search, sortBy, paging);

            var vehicleMakesDTO = new List<VehicleMakeDTO>();

            foreach (var vehicleMakes in vehicleMakesList)
            {
                vehicleMakesDTO.Add(_mapper.Map<VehicleMakeDTO>(vehicleMakes));
            }

            return Ok(vehicleMakesList);

        }

        [HttpGet("{vehicleMakeId:int}", Name = "GetVehicleMake")]
        [ProducesResponseType(200, Type = typeof(List<VehicleMakeDTO>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetVehicleMake(int vehicleMakeId)
        {
            var vehicleMake = _vmRepo.GetVehicleMake(vehicleMakeId);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            var vehicleMakeDTO = _mapper.Map<VehicleMakeDTO>(vehicleMake);
            return Ok(vehicleMakeDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateVehicleMake([FromBody] VehicleMakeDTO vehicleMakeDTO)
        {
            if (vehicleMakeDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_vmRepo.VehicleMakeExists(vehicleMakeDTO.Name))
            {
                ModelState.AddModelError("", "Vehicle Make Exists!");
                return StatusCode(404, ModelState);
            }

            var vehicleMakeObj = _mapper.Map<VehicleMake>(vehicleMakeDTO);

            if (!_vmRepo.CreateVehicleMake(vehicleMakeObj))
            {
                ModelState.AddModelError("", $"Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPatch("{vehicleMakeId:int}", Name = "UpdateVehicleMake")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateVehicleMake(int vehicleModelId, [FromBody] VehicleMakeDTO vehicleMakeDTO)
        {

            if (vehicleMakeDTO == null || vehicleModelId != vehicleMakeDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var vehicleMakeObj = _mapper.Map<VehicleMake>(vehicleMakeDTO);

            if (!_vmRepo.UpdateVehicleMake(vehicleMakeObj))
            {
                ModelState.AddModelError("", $"Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{vehicleMakeId:int}", Name = "DeleteVehicleMake")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteVehicleMake(int vehicleMakeId)
        {

            if (!_vmRepo.VehicleMakeExists(vehicleMakeId))
            {
                return NotFound();
            }

            var vehicleModelObj = _vmRepo.GetVehicleMake(vehicleMakeId);

            if (!_vmRepo.DeleteVehicleMake(vehicleModelObj))
            {
                ModelState.AddModelError("", $"Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
