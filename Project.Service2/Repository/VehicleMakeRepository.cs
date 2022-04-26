using Project.Service.Data;
using Project.Service.Models;
using Project.Service2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {

        private readonly ApplicationDbContext _db;

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

        public ICollection<VehicleMake> GetVehicleMakes(Filtering search, Sorting sortBy, Paging paging)
        {
            var vMakes = _db.vehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(search.SearchText))
            {
                vMakes = vMakes.Where(vm => vm.Name.Contains(search.SearchText)
                || vm.Abrv.Contains(search.SearchText));
            }

            if (!string.IsNullOrEmpty(sortBy.SortVehicleMake.ToString()))
            {
                switch (sortBy.SortVehicleMake.ToString())
                {
                    case "Name_Desc": vMakes = vMakes.OrderByDescending(vm => vm.Name); break;
                    case "Name_Asc": vMakes = vMakes.OrderBy(vm => vm.Name); break;
                    case "Abrv_Desc": vMakes = vMakes.OrderByDescending(vm => vm.Abrv); break;
                    case "Abrv_Asc": vMakes = vMakes.OrderBy(vm => vm.Abrv); break;
                }
            }

            vMakes = vMakes.Skip((paging.Page -1) * paging.PageSize).Take(paging.PageSize);

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
