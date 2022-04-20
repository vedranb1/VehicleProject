using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Models.DTOs;
using Project.Service.Repository.IRepository;
using System.Collections.Generic;

namespace Project.Service.Controllers
{
    [Route("api/v{version:apiVersion}/vehiclemodel")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {

        private readonly IVehicleModelRepository _vmRepo;
        private readonly IMapper _mapper;


        public VehicleModelController(IVehicleModelRepository vmRepo, IMapper mapper)
        {
            _vmRepo = vmRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<VehicleModelDTO>))]
        public IActionResult GetVehicleModels(string search, string sortBy, int page = 1)
        {

            var objList = _vmRepo.GetVehicleModels(search, sortBy, page);

            var objDTO = new List<VehicleModelDTO>();

            foreach (var obj in objList)
            {
                objDTO.Add(_mapper.Map<VehicleModelDTO>(obj));
            }

            return Ok(objList);

        }

        [HttpGet("{vehicleModelId:int}", Name = "GetVehicleModel")]
        [ProducesResponseType(200, Type = typeof(List<VehicleModelDTO>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetVehicleModel(int vehicleModelId)
        {
            var obj = _vmRepo.GetVehicleModel(vehicleModelId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDTO = _mapper.Map<VehicleModelDTO>(obj);
            return Ok(objDTO);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateVehicleModel([FromBody] VehicleModelDTO vehicleModelDTO)
        {

            if (vehicleModelDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_vmRepo.VehicleModelExists(vehicleModelDTO.Name))
            {
                ModelState.AddModelError("", "Vehicle Model Exists!");
                return StatusCode(404, ModelState);
            }

            var vehicleModelObj = _mapper.Map<VehicleModel>(vehicleModelDTO);

            if (!_vmRepo.CreateVehicleModel(vehicleModelObj))
            {
                ModelState.AddModelError("", $"Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPatch("{vehicleModelId:int}", Name = "UpdateVehicleModel")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateVehicleModel(int vehicleModelId, [FromBody] VehicleModelDTO vehicleModelDTO)
        {

            if (vehicleModelDTO == null || vehicleModelId != vehicleModelDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var vehicleModelObj = _mapper.Map<VehicleModel>(vehicleModelDTO);

            if (!_vmRepo.UpdateVehicleModel(vehicleModelObj))
            {
                ModelState.AddModelError("", $"Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{vehicleModelId:int}", Name = "DeleteVehicleModel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteVehicleModel(int vehicleModelId)
        {

            if (!_vmRepo.VehicleModelExists(vehicleModelId))
            {
                return NotFound();
            }

            var vehicleModelObj = _vmRepo.GetVehicleModel(vehicleModelId);

            if (!_vmRepo.DeleteVehicleModel(vehicleModelObj))
            {
                ModelState.AddModelError("", $"Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
