using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service2.Models
{
    [NotMapped]
    public class Sorting
    {

        public enum SortTypeVehicleModel {Name_Desc, Name_Asc, Abrv_Desc, Abrv_Asc, VehicleMakeName_Desc, VehicleMakeName_Asc}
        public enum SortTypeVehicleMake {Name_Desc, Name_Asc, Abrv_Desc, Abrv_Asc}
        public SortTypeVehicleModel SortVehicleModel { get; set; }
        public SortTypeVehicleMake SortVehicleMake { get; set; }

    }
}
