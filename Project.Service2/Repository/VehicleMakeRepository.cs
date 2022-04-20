using Project.Service.Data;
using Project.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {

        private readonly ApplicationDbContext _db;
        public static int PAGE_SIZE { get; set; } = 10;

        public VehicleMakeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateVehicleMake(VehicleMake vehicleMake)
        {
            _db.vehicleMakes.Add(vehicleMake);
            return Save();
        }

        public bool DeleteVehicleMake(VehicleMake vehicleMake)
        {
            _db.vehicleMakes.Remove(vehicleMake);
            return Save();
        }

        public VehicleMake GetVehicleMake(int vehicleMakeId)
        {
            return _db.vehicleMakes.FirstOrDefault(a => a.Id == vehicleMakeId);
        }

        public ICollection<VehicleMake> GetVehicleMakes(string search, string sortBy, int page)
        {
            var vMakes = _db.vehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                vMakes = vMakes.Where(vm => vm.Name.Contains(search)
                || vm.Abrv.Contains(search));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Name_Desc": vMakes = vMakes.OrderByDescending(vm => vm.Name); break;
                    case "Name_Asc": vMakes = vMakes.OrderBy(vm => vm.Name); break;
                    case "Abrv_Desc": vMakes = vMakes.OrderByDescending(vm => vm.Abrv); break;
                    case "Abrv_Asc": vMakes = vMakes.OrderBy(vm => vm.Abrv); break;
                }
            }

            vMakes = vMakes.Skip((page -1) * PAGE_SIZE).Take(PAGE_SIZE);

            return vMakes.ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateVehicleMake(VehicleMake vehicleMake)
        {
            _db.vehicleMakes.Update(vehicleMake);
            return Save();
        }

        public bool VehicleMakeExists(string name)
        {
            bool value = _db.vehicleMakes.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool VehicleMakeExists(int id)
        {
            bool value = _db.vehicleMakes.Any(a => a.Id == id);
            return value;
        }
    }
}
