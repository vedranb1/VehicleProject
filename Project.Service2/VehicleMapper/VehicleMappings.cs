using AutoMapper;
using Project.Service.Models;
using Project.Service.Models.DTOs;

namespace Project.Service.VehicleMapper
{
    public class VehicleMappings : Profile
    {

        public VehicleMappings()
        {
            CreateMap<VehicleMake, VehicleMakeDTO>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelDTO>().ReverseMap();
        }

    }
}
