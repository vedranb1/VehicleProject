using Project.Service.Models;
using Project.Service2.Models;
using System.Collections.Generic;

namespace Project.Service.Repository
{
    public interface IVehicleMakeRepository
    {

        ICollection<VehicleMake> GetVehicleMakes(Filtering search, Sorting sortBy, Paging paging);
        bool VehicleMakeExists(string name);
        bool VehicleMakeExists(int id);
        bool CreateVehicleMake(VehicleMake vehicleMake);
        bool UpdateVehicleMake(VehicleMake vehicleMake);
        bool DeleteVehicleMake(VehicleMake vehicleMake);
        VehicleMake GetVehicleMake(int vehicleMakeId);
        bool Save();

    }
}
