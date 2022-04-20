using Project.Service.Models;
using System.Collections.Generic;

namespace Project.Service.Repository.IRepository
{
    public interface IVehicleModelRepository
    {

        ICollection<VehicleModel> GetVehicleModels(string search, string sortBy, int page);
        bool VehicleModelExists(string name);
        bool VehicleModelExists(int id);
        bool CreateVehicleModel(VehicleModel vehicleModel);
        bool UpdateVehicleModel(VehicleModel vehicleModel);
        bool DeleteVehicleModel(VehicleModel vehicleModel);
        VehicleModel GetVehicleModel(int vehicleModelId);
        bool Save();

    }
}
