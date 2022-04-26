using Project.Service.Models;
using Project.Service2.Models;
using System.Collections.Generic;

namespace Project.Service.Repository.IRepository
{
    public interface IVehicleModelRepository
    {

        ICollection<VehicleModel> GetVehicleModels(Filtering search, Sorting sortBy, Paging paging);
        bool VehicleModelExists(string name);
        bool VehicleModelExists(int id);
        bool CreateVehicleModel(VehicleModel vehicleModel);
        bool UpdateVehicleModel(VehicleModel vehicleModel);
        bool DeleteVehicleModel(VehicleModel vehicleModel);
        VehicleModel GetVehicleModel(int vehicleModelId);
        bool Save();

    }
}
