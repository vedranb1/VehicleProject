using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {

        private readonly ApplicationDbContext _db;
        public static int PAGE_SIZE { get; set; } = 10;

        public VehicleModelRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateVehicleModel(VehicleModel vehicleModel)
        {
            _db.vehicleModels.Add(vehicleModel);
            return Save();
        }

        public bool DeleteVehicleModel(VehicleModel vehicleModel)
        {
            _db.vehicleModels.Remove(vehicleModel);
            return Save();
        }

        public VehicleModel GetVehicleModel(int vehicleModelId)
        {
            return _db.vehicleModels.FirstOrDefault(a => a.Id == vehicleModelId);
        }

        public ICollection<VehicleModel> GetVehicleModels(string search, string sortBy, int page)
        {
            var vModels = _db.vehicleModels.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                vModels = vModels.Where(vm => vm.Name.Contains(search)
                || vm.Abrv.Contains(search)
                || vm.VehicleMake.Name.Contains(search));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Name_Desc": vModels = vModels.OrderByDescending(vm => vm.Name); break;
                    case "Name_Asc": vModels = vModels.OrderBy(vm => vm.Name); break;
                    case "Abrv_Desc": vModels = vModels.OrderByDescending(vm => vm.Abrv); break;
                    case "Abrv_Asc": vModels = vModels.OrderBy(vm => vm.Abrv); break;
                    case "VehicleMakeName_Desc": vModels = vModels.OrderByDescending(vm => vm.VehicleMake.Name); break;
                    case "VehicleMakeName_Asc": vModels = vModels.OrderBy(vm => vm.VehicleMake.Name); break;
                }
            }

            vModels = vModels.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            vModels = vModels.Include(vm => vm.VehicleMake);

            return vModels.ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateVehicleModel(VehicleModel vehicleModel)
        {
            _db.vehicleModels.Update(vehicleModel);
            return Save();
        }

        public bool VehicleModelExists(string name)
        {
            bool value = _db.vehicleModels.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool VehicleModelExists(int id)
        {
            bool value = _db.vehicleModels.Any(a => a.Id == id);
            return value;
        }
    }
}
